namespace API.Helpers
{
    public class PaginationParams
    {
        private const int _maxPageSize = 25;
        public int PageNumber {get; set;} = 1;
        private int _pageSize = 15;
        public int PageSize{
            get =>_pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }
    }
}