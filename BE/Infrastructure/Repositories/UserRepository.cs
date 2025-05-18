namespace Infrastructure;

internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    #region Declaration
    #endregion

    #region Property
    #endregion

    #region Constructor
    public UserRepository(RepositoryContext context)
        : base(context) { }
    #endregion

    #region Method
    #endregion
}
