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
    internal sealed class BookingDetailRepository : RepositoryBase<BookingDetail>, IBookingDetailRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public BookingDetailRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Method
        #endregion
    }
}
