using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Business
{
    internal sealed class PostService : ServiceBase<Post>, IPostService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public PostService(IRepositoryManager repository,
            ILoggerManager logger,
            IMapper mapper) : base(repository.Post)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var posts = await _repository.Post.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<PostDto>>(posts),
                StatusCode = !posts.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var post = await _repository.Post.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<PostDto>(post),
                StatusCode = post is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> DeleteAsync(string id)
        {
            var postDelete = await _repository.Post.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (postDelete is not null)
            {
                _repository.Post.Delete(postDelete);
                await _repository.SaveAsync();
                return new Response
                {
                    Success = true,
                    Messages = Successfull.DeleteSucceed,
                    StatusCode = (int)HttpStatusCode.NoContent
                };
            }
            else
            {
                throw new PostNotFoundException(id);
            }
        }
        public async Task<Response> InsertAsync(PostDto postDto)
        {
            await ValidateObject(postDto);

            var post = _mapper.Map<Post>(postDto);
            _repository.Post.Create(post);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> UpdateAsync(string id, PostDto postDto)
        {
            await ValidateObject(postDto);

            var post = await _repository.Post.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (post is not null)
            {
                _mapper.Map(postDto, post);
                _repository.Post.Update(post);
                await _repository.SaveAsync();
            }
            else
            {
                throw new PostNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public Task ValidateObject(PostDto postDto)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
