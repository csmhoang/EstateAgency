using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        #region Declaration
        private readonly RepositoryContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRoomRepository> _roomRepository;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(context));
        }
        #endregion

        #region Method
        public IUserRepository User => _userRepository.Value;
        public IRoomRepository Room => _roomRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
        #endregion

    }
}
