using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Data
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        /// <summary>
        /// Lấy ra chi tiết người dùng bằng id
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>
        /// 1 - Người dùng
        /// 2 - Null
        /// </returns>
        IQueryable<User> GetDetail(string id);
    }
}
