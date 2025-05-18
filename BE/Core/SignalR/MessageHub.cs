using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Core.SignalR;

public class MessageHub : Hub
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;

    public MessageHub(IMapper mapper, IRepositoryManager repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var conversationId = httpContext?.Request.Query["conversationId"].ToString();
        if (string.IsNullOrEmpty(conversationId))
        {
            throw new HubException(Invalidate.IdRequired);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        var messages = await _repository.Conversation
            .FindCondition(c => c.Id.Equals(conversationId))
            .Include(c => c.Messages!)
            .ThenInclude(m => m.Sender!)
            .Include(c => c.Messages!)
            .ThenInclude(m => m.Receiver!)
            .SelectMany(c => c.Messages)
            .OrderBy(m => m.SentAt)
            .ToListAsync();

        await Clients.Caller.SendAsync("ReceiveMessagesThread", _mapper.Map<IEnumerable<MessageDto>>(messages));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    [Authorize]
    public async Task SendMessage(MessageDto messageDto)
    {
        var newMessage = _mapper.Map<Entities.Message>(messageDto);
        _repository.Message.Create(newMessage);
        await _repository.SaveAsync();

        var message = await _repository.Message
            .FindCondition(m => m.Id.Equals(newMessage.Id))
            .Include(m => m.Sender!)
            .Include(m => m.Receiver!)
            .FirstOrDefaultAsync();

        await Clients.Group(newMessage.ConversationId!).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
    }
}
