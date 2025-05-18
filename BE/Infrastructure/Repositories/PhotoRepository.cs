namespace Infrastructure;

internal sealed class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
{
    #region Declaration
    #endregion

    #region Property
    #endregion

    #region Constructor
    public PhotoRepository(RepositoryContext context)
        : base(context) { }
    #endregion

    #region Method
    #endregion
}
