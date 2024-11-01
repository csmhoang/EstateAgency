using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Params;
using Core.Resources;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Core.Enums.PostEnums;

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
        
        public async Task<Response> GetListAsync(PostSpecParams specParams)
        {
            var spec = new PostSpecification(specParams);
            var page = await CreatePagedResult(spec, specParams.PageIndex, specParams.PageSize);
            return new Response
            {
                Success = true,
                Data = new
                {
                    page.PageIndex,
                    page.PageSize,
                    page.Count,
                    Data = _mapper.Map<IEnumerable<PostDto>>(page.Data)
                },
                StatusCode = !page.Data.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
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

        public async Task<Response> UpdateAsync(string id, PostUpdateDto postUpdateDto)
        {
            await ValidateObject(postUpdateDto);

            var post = await _repository.Post.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (post is not null)
            {
                _mapper.Map(postUpdateDto, post);
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
        public Task ValidateObject<T>(T model)
        {
            return Task.CompletedTask;
        }

        public async Task<Response> RemoveAsync(string id)
        {
            var post = await _repository.Post.FindCondition(r => r.Id.Equals(id))
               .FirstOrDefaultAsync();
            if (post is not null)
            {
                post.Status = StatusPost.Deleted;
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
                Messages = Successfull.RemovePost,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        #endregion
    }
}
