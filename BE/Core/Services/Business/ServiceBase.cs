using Core.Dtos;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Data;
using Core.Interfaces.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        protected async Task<Pagination<T>> CreatePagedResult(ISpecification<T> spec,
            int pageIndex, int pageSize)
        {
            var items = await _repository.ListAsync(spec);
            var count = await _repository.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
            return pagination;
        }
        #endregion


    }
}
