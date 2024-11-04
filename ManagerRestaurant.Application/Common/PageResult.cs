namespace ManagerRestaurant.Application.Common
{
    public class PageResult<T>
    {
        public PageResult(IEnumerable<T> items, int totalCount,int pageSize,int pageNumber)
        {
            Items = items;
            TotalPage =(int) Math.Ceiling(totalCount / (double)pageSize);
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1)+1;
            ItemsTo = ItemsFrom + pageSize -1;
        }

        public IEnumerable<T> Items { get; set; }
        public int TotalPage { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo {get; set;}
    }
}
