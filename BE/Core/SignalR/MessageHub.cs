using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Data;
using Core.Resources;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SignalR
{
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
            var otherUser = httpContext?.Request.Query["user"].ToString();
            var postId = httpContext?.Request.Query["postId"].ToString();
            if (string.IsNullOrEmpty(postId))
            {
                throw new HubException(Invalidate.IdRequired);
            }

            var spec = new BaseSpecification<Feedback>(f =>
                f.PostId!.Equals(postId) && string.IsNullOrEmpty(f.ReplyId)
            );
            spec.AddInclude(x => x.Include(f => f.Tenant!));
            spec.AddInclude(x => x.Include(f => f.Replies!));
            spec.AddOrder(x => x.OrderBy(f => f.CreatedAt));
            var data = await _repository.Feedback.ListAsync(spec);

            await Clients.Caller.SendAsync("ReceiveFeedbacksThread", _mapper.Map<IEnumerable<FeedbackDto>>(data));
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        [Authorize]
        public async Task SendFeedback(FeedbackDto feedbackDto)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDto);
            _repository.Feedback.Create(feedback);
            await _repository.SaveAsync();

            var spec = new BaseSpecification<Feedback>(f =>
                f.Id.Equals(feedback.Id)
            );
            spec.AddInclude(x => x.Include(f => f.Tenant!));
            spec.AddInclude(x => x.Include(f => f.Replies!));
            var data = await _repository.Feedback.GetEntityWithSpec(spec);

            await Clients.All.SendAsync("NewFeedback", _mapper.Map<FeedbackDto>(data));
        }
    }
}
