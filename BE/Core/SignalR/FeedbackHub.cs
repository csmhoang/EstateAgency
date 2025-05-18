using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class FeedbackHub : Hub
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;

    public FeedbackHub(IMapper mapper, IRepositoryManager repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var postId = httpContext?.Request.Query["postId"].ToString();
        if (string.IsNullOrEmpty(postId))
        {
            throw new HubException(Invalidate.IdRequired);
        }

        var feedbacks = await _repository.Feedback
            .FindCondition(f => f.PostId!.Equals(postId) && string.IsNullOrEmpty(f.ReplyId))
            .Include(f => f.Tenant!)
            .Include(f => f.Replies!)
            .OrderBy(f => f.CreatedAt)
            .ToListAsync();

        await Clients.Caller.SendAsync("ReceiveFeedbacksThread", _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    [Authorize]
    public async Task SendFeedback(FeedbackDto feedbackDto)
    {
        var newFeedback = _mapper.Map<Feedback>(feedbackDto);
        _repository.Feedback.Create(newFeedback);
        await _repository.SaveAsync();

        var feedback = await _repository.Feedback
            .FindCondition(f => f.Id.Equals(newFeedback.Id))
            .Include(f => f.Tenant!)
            .Include(f => f.Replies!)
            .FirstOrDefaultAsync();

        await Clients.All.SendAsync("NewFeedback", _mapper.Map<FeedbackDto>(feedback));
    }
}
