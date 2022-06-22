namespace MyWebApi.Services
{
    public class PageList<T> : List<T>
    {
        public PageList(List<T> items, int count, int pageIndex, int PageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(items);
        }

        public int PageIndex { get; set; } 
        public int TotalPage { get; set; } 

        public static PageList<T> Create(IQueryable<T> source, int pageIndex, int PageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1)*PageSize).Take(PageSize).ToList();
            return new PageList<T>(items, count, pageIndex, PageSize);
        }
    }
}
