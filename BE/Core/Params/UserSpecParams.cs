namespace Core;

public class UserSpecParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    private List<string> _roles = new();
    public List<string> Roles
    {
        get => _roles;
        set
        {
            _roles = value.SelectMany(x => x.Split(",",
                StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }
}
