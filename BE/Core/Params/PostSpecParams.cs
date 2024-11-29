using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.RoomEnums;

namespace Core.Params
{
    public class PostSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        private List<string> _landlordIds = new();
        public List<string> LandlordId
        {
            get => _landlordIds;
            set
            {
                _landlordIds = value.SelectMany(x => x.Split(",",
                    StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }
        private List<string> _provinces = new();
        public List<string> Province
        {
            get => _provinces;
            set
            {
                _provinces = value.SelectMany(x => x.Split(",",
                    StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public decimal? MinArea { get; set; }
        public decimal? MaxArea { get; set; }

        public Category? Category { get; set; }

        public string? SortPrice { get; set; }
        public string? SortArea { get; set; }
        public string? SortExtra { get; set; }

        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }
    }
}
