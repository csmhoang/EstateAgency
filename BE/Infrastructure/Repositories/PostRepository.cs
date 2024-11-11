using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public PostRepository(RepositoryContext context)
            : base(context) { }

        #endregion

        #region Method
        public IQueryable<Post> GetDetail(string id) =>
            _context.Posts
                .Include(p => p.Room!)
                .ThenInclude(r => r.Landlord)
                .Include(p => p.Room!)
                .ThenInclude(r => r.Photos)
                .Where(p => p.Id.Equals(id));
        public IQueryable<Post> GetAllDetail() =>
            _context.Posts
                .Include(p => p.Room!);

        #endregion
    }
}
