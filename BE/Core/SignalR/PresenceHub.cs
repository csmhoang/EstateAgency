using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.SignalR;

public class PresenceHub : Hub
{
    private readonly PresenceTracker _tracker;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;

    public PresenceHub(PresenceTracker tracker, IMapper mapper, IRepositoryManager repository)
    {
        _tracker = tracker;
        _mapper = mapper;
        _repository = repository;
    }
    public override async Task OnConnectedAsync()
    {
        if (Context.User != null)
        {
            var userId = Context.User.GetUserId();
            var isOnline = await _tracker.UserConnected(userId, Context.ConnectionId);
            if (isOnline)
                await Clients.All.SendAsync("UserIsOnline", Context.User.GetUserId());
            var conversations = await _repository.Participant
                .FindCondition(p => p.UserId!.Equals(userId))
                .Include(p => p.Conversation!)
                .ThenInclude(c => c.Participants!)
                .ThenInclude(p => p.User!)
                .Select(p => p.Conversation!)
                .ToListAsync();
            await Clients.Caller.SendAsync("ReceiveConversationsThread", _mapper.Map<IEnumerable<ConversationDto>>(conversations));

            var notifications = await _repository.Notification
                .FindCondition(n => n.ReceiverId!.Equals(userId))
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            await Clients.Caller.SendAsync("ReceiveNotificationsThread", _mapper.Map<IEnumerable<NotificationDto>>(notifications));
        }
        var currentUsers = await _tracker.GetOnlineUsers();
        await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);

    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.User == null) throw new HubException(Failure.NotConnectHub);

        var isOffline = await _tracker.UserDisconnected(Context.User.GetUserId(), Context.ConnectionId);
        if (isOffline)
            await Clients.All.SendAsync("UserIsOffline", Context.User.GetUserId());

        await base.OnDisconnectedAsync(exception);
    }

    public async Task CreateNotification(NotificationDto notificationDto)
    {
        var notification = _mapper.Map<Notification>(notificationDto);
        _repository.Notification.Create(notification);
        await _repository.SaveAsync();

        var OtherConnectionIds = await _tracker.GetConnectionsForUser(notificationDto.ReceiverId);
        await Clients.Clients(OtherConnectionIds).SendAsync("NewNotification", _mapper.Map<NotificationDto>(notification));
    }

    public async Task DeleteNotification(string notificationId)
    {
        var notification = await _repository.Notification
            .FindCondition(m => m.Id.Equals(notificationId))
            .FirstOrDefaultAsync();
        if (notification != null)
        {
            _repository.Notification.Delete(notification);
            await _repository.SaveAsync();

            var OtherConnectionIds = await _tracker.GetConnectionsForUser(notification.ReceiverId!);
            await Clients.Clients(OtherConnectionIds).SendAsync("DeleteNotification", notificationId);
        }
    }

    public async Task<Response> GetConversation(string callerId, string otherId)
    {
        var conversationId = await
            _repository.Participant.FindCondition(p => p.UserId!.Equals(callerId))
            .Select(p => p.ConversationId!)
            .Intersect(
                _repository.Participant.FindCondition(p => p.UserId!.Equals(otherId))
                .Select(p => p.ConversationId!)
            )
            .FirstOrDefaultAsync();

        var conversation = await _repository.Conversation
            .FindCondition(c => c.Id.Equals(conversationId))
            .Include(c => c.Participants!)
            .ThenInclude(p => p.User!)
            .FirstOrDefaultAsync();

        if (conversation == null)
        {
            conversation = new Conversation();
            conversation.Participants.Add(new Participant { UserId = callerId, ConversationId = conversationId });
            conversation.Participants.Add(new Participant { UserId = otherId, ConversationId = conversationId });
            _repository.Conversation.Create(conversation); await _repository.SaveAsync();

            conversation = await _repository.Conversation
                .FindCondition(c => c.Id.Equals(conversation.Id))
                .Include(c => c.Participants!)
                .ThenInclude(p => p.User!)
                .FirstOrDefaultAsync();

            var OtherConnectionIds = await _tracker.GetConnectionsForUser(otherId);
            OtherConnectionIds.Add(Context.ConnectionId);
            await Clients.Clients(OtherConnectionIds).SendAsync("NewConversation", _mapper.Map<ConversationDto>(conversation));
        }

        return new Response
        {
            Success = true,
            Data = _mapper.Map<ConversationDto>(conversation),
            StatusCode = conversation == null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }
}
