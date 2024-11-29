using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public InvoiceRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Method
        #endregion
    }
}
