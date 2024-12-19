using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
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

        public async Task<Response> GetAsync(string cartId)
        {
            var cartDetail = await _repository.CartDetail.FindCondition(r => r.Id.Equals(cartId))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<CartDetailDto>(cartDetail),
                StatusCode = cartDetail is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> RemoveAsync(string cartDetailId)
        {
            var cartDetailDelete = await _repository.CartDetail.FindCondition(r => r.Id.Equals(cartDetailId))
                .FirstOrDefaultAsync();
            if (cartDetailDelete == null) throw new CartDetailNotFoundException(cartDetailId);

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

        public async Task<Response> CartCurrent(string userId)
        {
            var cart = string.IsNullOrEmpty(userId) ? null :
                await _repository.Cart.FindCondition(c => c.TenantId!.Equals(userId))
                .Include(c => c.CartDetails!)
                .ThenInclude(cd => cd.Room!)
                .ThenInclude(r => r.Photos!)
                .FirstOrDefaultAsync();

            return new Response
            {
                Success = true,
                Data = _mapper.Map<CartDto>(cart),
                StatusCode = cart is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> UpdateAsync(string cartId, CartDto cartDto)
        {
            var cart = await _repository.Cart.FindCondition(r => r.Id.Equals(cartId))
                .Include(c => c.CartDetails!)
                .FirstOrDefaultAsync();
            if (cart == null) throw new CartNotFoundException(cartId);

            _mapper.Map(cartDto, cart);
            foreach (var cartDetail in cart.CartDetails)
            {
                cartDetail.UpdatedAt = DateTime.Now;
                _repository.CartDetail.Update(cartDetail);
            }
            await _repository.SaveAsync();
            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public Task ValidateObject(CartDetailDto cartDetailDto)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
