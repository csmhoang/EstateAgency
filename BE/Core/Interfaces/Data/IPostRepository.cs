using Core.Dtos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Data
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        /// <summary>
        /// Lấy ra chi tiết bài đăng bằng id
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <returns>
        /// 1 - Bài đăng
        /// 2 - Null
        /// </returns>
        IQueryable<Post> GetDetail(string id);
    }
}
