using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class SavePostRepository : RepositoryBase<SavePost>, ISavePostRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public SavePostRepository(RepositoryContext context)
            : base(context) { }

        #endregion

        #region Method
        #endregion
    }
}
