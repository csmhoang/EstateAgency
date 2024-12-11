using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    internal sealed class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public MessageRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Method
        #endregion
    }
}
