namespace Worktop.Core.Application.Params
{
    public abstract class FiltersParams
    {
        protected const int MaxPageSize = int.MaxValue;
        protected const int MinPageNumber = 1;

        protected int pageNumber = MinPageNumber;
        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = (value < MinPageNumber) ? MinPageNumber : value;
        }

        protected int pageSize = 5;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public FiltersParams CurrentPage(int currentPage)
        {
            PageNumber = currentPage;
            return this;
        }
    }
}