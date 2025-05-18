using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core;

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

    public async Task<Response> GetDetailAsync(string id)
    {
        var post = await _repository.Post.FindCondition(p => p.Id.Equals(id))
            .Include(p => p.Landlord!)
            .ThenInclude(l => l.Followers!)
            .Include(p => p.Room!)
            .ThenInclude(r => r.Photos!)
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

    public async Task<Response> GetListRecentAsync()
    {
        var spec = new BaseSpecification<Post>();
        spec.AddInclude(x => x
            .Include(p => p.Room!)
            .ThenInclude(r => r.Photos!)
        );
        spec.AddOrder(x => x
            .OrderBy(p => p.CreatedAt));
        spec.ApplyPaging(0, 6);

        var posts = await _repository.Post.ListAsync(spec);
        return new Response
        {
            Success = true,
            Data = _mapper.Map<IEnumerable<PostDto>>(posts),
            StatusCode = !posts.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> DeleteAsync(string id)
    {
        var postDelete = await _repository.Post.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if(postDelete is not null) {
            _repository.Post.Delete(postDelete);
            await _repository.SaveAsync();
            return new Response
            {
                Success = true,
                Messages = Successfull.DeleteSucceed,
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }
        else {
            throw new PostNotFoundException(id);
        }
    }
    public async Task<Response> InsertAsync(PostDto postDto)
    {
        await ValidateObject(postDto);
        var room = await _repository.Room
            .FindCondition(r => r.Id.Equals(postDto.RoomId))
            .FirstOrDefaultAsync();
        if(room == null)
            throw new RoomNotFoundException(postDto.RoomId);
        room.Condition = RoomEnums.ConditionRoom.PostingForRent;

        var post = _mapper.Map<Post>(postDto);
        _repository.Post.Create(post);
        _repository.Room.Update(room);
        await _repository.SaveAsync();
        return new Response
        {
            Success = true,
            Messages = Successfull.InsertSucceed,
            StatusCode = (int)HttpStatusCode.Created
        };
    }

    public async Task<Response> SavePostAsync(SavePostDto savePostDto, bool isSave)
    {
        var post = await _repository.Post
            .FindCondition(r => r.Id.Equals(savePostDto.PostId))
            .FirstOrDefaultAsync();
        if(post == null)
            throw new PostNotFoundException(savePostDto.PostId!);

        var user = await _repository.User
            .FindCondition(r => r.Id.Equals(savePostDto.UserId))
            .FirstOrDefaultAsync();
        if(user == null)
            throw new UserNotFoundException();

        var savePost = await _repository.SavePost
            .FindCondition(r =>
                r.UserId!.Equals(savePostDto.UserId) &&
                r.PostId!.Equals(savePostDto.PostId)
            )
            .FirstOrDefaultAsync();

        if(isSave) {
            if(savePost == null) {
                var savePostInsert = _mapper.Map<SavePost>(savePostDto);
                _repository.SavePost.Create(savePostInsert);
                await _repository.SaveAsync();
            }
        }
        else {
            if(savePost != null) {
                _repository.SavePost.Delete(savePost);
                await _repository.SaveAsync();
            }
        }

        return new Response
        {
            Success = true,
            Messages = isSave ? Successfull.SaveSucceed : Successfull.CancelSucceed,
            StatusCode = (int)HttpStatusCode.NoContent
        };
    }

    public async Task<Response> UpdateAsync(string id, PostUpdateDto postUpdateDto)
    {
        await ValidateObject(postUpdateDto);

        var post = await _repository.Post.FindCondition(p => p.Id.Equals(id))
            .FirstOrDefaultAsync();
        if(post is not null) {
            _mapper.Map(postUpdateDto, post);
            post.UpdatedAt = DateTime.Now;
            _repository.Post.Update(post);
            await _repository.SaveAsync();
        }
        else {
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
        if(post is not null) {
            var room = await _repository.Room
                .FindCondition(r => r.Id.Equals(post.RoomId))
                .FirstOrDefaultAsync();
            if(room == null)
                throw new RoomNotFoundException(post.RoomId!);
            room.Condition = RoomEnums.ConditionRoom.Available;

            post.Status = PostEnums.StatusPost.Deleted;
            _repository.Room.Update(room);
            _repository.Post.Update(post);
            await _repository.SaveAsync();
        }
        else {
            throw new PostNotFoundException(id);
        }

        return new Response
        {
            Success = true,
            Messages = Successfull.RemovePost,
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> ResponseAsync(string id, PostEnums.IsAcceptPost isAccept)
    {
        var post = await _repository.Post.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if(post == null)
            throw new PostNotFoundException(id);
        post.IsAccept = isAccept;
        post.UpdatedAt = DateTime.Now;
        _repository.Post.Update(post);
        await _repository.SaveAsync();
        return new Response
        {
            Success = true,
            Messages = Successfull.ResponseSucceed,
            StatusCode = (int)HttpStatusCode.NoContent
        };
    }

    public async Task<Response> GetSearchOptionsAsync()
    {
        var post = await _repository.Post.FindAll()
            .Include(p => p.Room!)
            .ToListAsync();

        var options = post.Select(p => new
        {
            p.Title,
            p.Room!.Name,
            p.Room!.Address
        });

        return new Response
        {
            Success = true,
            Data = options.SelectMany(o => new[] { o.Title, o.Name, o.Address }),
            StatusCode = !options.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }
    #endregion
}
