using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.RoomEnums;

namespace Infrastructure.Repositories
{
    internal sealed class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public RoomRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Method
        #endregion
    }
}
