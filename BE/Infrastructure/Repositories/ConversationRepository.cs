using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    internal sealed class ConversationRepository : RepositoryBase<Conversation>, IConversationRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ConversationRepository(RepositoryContext context)
            : base(context) { }
        #endregion

        #region Method
        #endregion
    }
}
