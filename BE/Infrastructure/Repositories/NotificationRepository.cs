using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    internal sealed class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public NotificationRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Method
        #endregion
    }
}
