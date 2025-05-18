namespace Infrastructure;

internal sealed class PostRepository : RepositoryBase<Post>, IPostRepository
{
    #region Declaration
    #endregion

    #region Property
    #endregion

    #region Constructor
    public PostRepository(RepositoryContext context)
        : base(context) { }

    #endregion

    #region Method
    #endregion
}
