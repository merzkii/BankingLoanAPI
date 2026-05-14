namespace Application.DTO.Common
{
    public abstract record PagedQueryParameters
    {
        private const int MaxPageSize = 100;
        private int _pageNumber = 1;
        private int _pageSize = 10;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = value switch
            { 
                <= 1 => 10,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }

        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = "asc"; 

        public bool SortDescending => string.Equals(SortDirection, "desc", StringComparison.OrdinalIgnoreCase);
    }
}
