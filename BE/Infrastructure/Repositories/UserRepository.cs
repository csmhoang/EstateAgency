using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public UserRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Method
        public IQueryable<User> GetDetail(string id) =>
            _context.Users
        .Include(u => u.Rooms)
        .ThenInclude(p => p.Posts)
        .Where(p => p.Id.Equals(id));
        #endregion
    }
}
