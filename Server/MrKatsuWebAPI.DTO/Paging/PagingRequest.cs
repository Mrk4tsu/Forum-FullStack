namespace MrKatsuWebAPI.DTO.Paging
{
    public class PagingRequest
    {
        private int _pageIndex = 1;
        private int _pageSize = 12;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value > 0 ? value : 1;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : 12;
        }
    }
}
