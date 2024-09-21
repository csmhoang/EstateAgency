using Core.Interfaces.Data;

namespace Core.Services.Business
{
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
        //protected async Task<string> NewCode()
        //{
        //    var order = 1;
        //    var className = typeof(T).Name;
        //    var last = await _repository.FindAll().LastOrDefaultAsync();
        //    if (last is not null)
        //    {
        //        var code = last?.GetType().GetProperty($"{className}Code")?.GetValue(last)?.ToString();
        //        order = Convert.ToInt32(code?.Substring(className.Length)) + 1;
        //    }
        //    return string.Concat(className.ToUpper(), order);
        //}
        #endregion


    }
}
