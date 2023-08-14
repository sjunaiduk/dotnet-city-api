namespace CityInfo.API.Services
{
    public class PageMetadata
    {
        public int _pageSize { get; }
        public  int _currentPage { get; }
        public  int _totalItems { get; }



        public int _totalPages { get => (int)Math.Ceiling(_totalItems / (double)_pageSize); }

        public PageMetadata(int pageSize, int totalItems, int currentPage)
        {
            _pageSize = pageSize;
            _currentPage = currentPage;
            _totalItems = totalItems;
        }
    }
}
