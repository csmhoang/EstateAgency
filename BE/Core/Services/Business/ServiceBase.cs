namespace Core;

abstract class ServiceBase<T> where T : class
{
    #region Declaration
    private readonly IRepositoryBase<T> _repository;
    #endregion

    #region Property
    #endregion

    #region Constructor
    protected ServiceBase(IRepositoryBase<T> repository)
    {
        _repository = repository;
    }
    #endregion

    #region Method
    protected async Task<Pagination<T>> CreatePagedResult(ISpecification<T> spec,
        int pageIndex, int pageSize)
    {
        var items = await _repository.ListAsync(spec);
        var count = await _repository.CountAsync(spec);
        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
        return pagination;
    }
    protected async Task<Pagination<TResult>> CreatePagedResult<TResult>(ISpecification<T, TResult> spec,
        int pageIndex, int pageSize)
    {
        var items = await _repository.ListAsync(spec);
        var count = await _repository.CountAsync(spec);
        var pagination = new Pagination<TResult>(pageIndex, pageSize, count, items);
        return pagination;
    }
    #endregion
}
