﻿namespace Core;

public class BookingDetailSpecParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    private List<string> _tenantIds = new();
    public List<string> TenantId
    {
        get => _tenantIds;
        set
        {
            _tenantIds = value.SelectMany(x => x.Split(",",
                StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
    public string? Sort { get; set; }

    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }
}
