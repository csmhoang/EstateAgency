using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Resources;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Services.Business
{
    internal class CartService : ServiceBase<Cart>, ICartService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public CartService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.Cart)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var cartDetails = await _repository.CartDetail.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<CartDetailDto>>(cartDetails),
                StatusCode = !cartDetails.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var cartDetail = await _repository.CartDetail.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<CartDetailDto>(cartDetail),
                StatusCode = cartDetail is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> RemoveAsync(string id)
        {
            var cartDetailDelete = await _repository.CartDetail.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (cartDetailDelete == null) throw new CartDetailNotFoundException(id);

            _repository.CartDetail.Delete(cartDetailDelete);
            await _repository.SaveAsync();
            return new Response
            {
                Success = true,
                Messages = Successfull.DeleteSucceed,
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }

        public async Task<Response> AppendAsync(CartDetailDto cartDetailDto)
        {
            var cart = await _repository.Cart.FindCondition(r => r.Id.Equals(cartDetailDto.CartId))
                .FirstOrDefaultAsync();
            if (cart == null) throw new CartNotFoundException(cartDetailDto.CartId);

            var cartDetail = _mapper.Map<CartDetail>(cartDetailDto);
            _repository.CartDetail.Create(cartDetail);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public Task ValidateObject(CartDetailDto cartDetailDto)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
