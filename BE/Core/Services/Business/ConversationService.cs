using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Services.Business
{
    internal class ConversationService : ServiceBase<Conversation>, IConversationService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ConversationService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.Conversation)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllByUserIdAsync(string userId)
        {
            var conversations = await _repository.Participant
                .FindCondition(p => p.UserId!.Equals(userId))
                .Include(p => p.Conversation!)
                .ThenInclude(c => c.Participants!)
                .ThenInclude(p => p.User!)
                .Select(p => p.Conversation!)
                .ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<ConversationDto>>(conversations),
                StatusCode = !conversations.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetByTwoUserIdAsync(string callerId, string otherId)
        {
            var conversationId = await
                _repository.Participant.FindCondition(p => p.UserId!.Equals(callerId))
                .Select(p => p.ConversationId!)
                .Intersect(
                    _repository.Participant.FindCondition(p => p.UserId!.Equals(otherId))
                    .Select(p => p.ConversationId!)
                )
                .FirstOrDefaultAsync();

            var conversation = await _repository.Conversation.FindCondition(c => c.Id.Equals(conversationId))
                .FirstOrDefaultAsync();
            if (conversation == null)
            {
                conversation = new Conversation();
                conversation.Participants.Add(new Participant { UserId = callerId, ConversationId = conversationId });
                conversation.Participants.Add(new Participant { UserId = otherId, ConversationId = conversationId });
                _repository.Conversation.Create(conversation);
                await _repository.SaveAsync();
            }
            return new Response
            {
                Success = true,
                Data = _mapper.Map<ConversationDto>(conversation),
                StatusCode = conversation == null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        #endregion
    }
}
