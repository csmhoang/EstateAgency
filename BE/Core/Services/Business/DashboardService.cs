using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core;

internal sealed class DashboardService : IDashboardService
{
    #region Declaration
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public DashboardService(IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    #endregion

    #region Method
    public async Task<Response> RoomCountAsync(string landlordId)
    {
        var roomCount = await _repository.Room
            .FindCondition(r =>
                r.LandlordId!.Equals(landlordId) &&
                r.Visibility != null && r.Visibility == true
            )
            .CountAsync();

        return new Response
        {
            Success = true,
            Data = roomCount,
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> RoomBlankCountAsync(string landlordId)
    {
        var roomCount = await _repository.Room
            .FindCondition(r =>
                r.LandlordId!.Equals(landlordId) &&
                r.Condition != RoomEnums.ConditionRoom.Occupied &&
                r.Visibility != null && r.Visibility == true
            )
            .CountAsync();

        return new Response
        {
            Success = true,
            Data = roomCount,
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> TenantCountAsync(string landlordId)
    {
        var tenantCount = await _repository.LeaseDetail
            .FindCondition(ld =>
                ld.Room!.LandlordId!.Equals(landlordId) &&
                ld.Lease!.Status == LeaseEnums.StatusLease.Active
            ).SumAsync((ld => ld.NumberOfTenant));

        return new Response
        {
            Success = true,
            Data = tenantCount,
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> RevenueAsync(string landlordId)
    {
        var Revenue = await _repository.LeaseDetail
            .FindCondition(ld =>
                ld.Room!.LandlordId!.Equals(landlordId) &&
                (
                    ld.Lease!.Status == LeaseEnums.StatusLease.Active ||
                    ld.Lease!.Status == LeaseEnums.StatusLease.Expired
                )
            ).SumAsync((ld => ld.Price));

        return new Response
        {
            Success = true,
            Data = Revenue,
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> VisitCountAsync(int numberOfMonth)
    {
        var visitCount = await _repository.VisitStat
            .FindCondition(
                v => v.Year == DateTime.Now.Year &&
                v.Month >= DateTime.Now.Month - numberOfMonth
            ).SumAsync(v => v.VisitCount);

        return new Response
        {
            Success = true,
            Data = visitCount,
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    #endregion
}
