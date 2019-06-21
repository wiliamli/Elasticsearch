namespace Jwell.Framework.Paging
{
    public class PageParam
    {
        private int pageIndex = 1;
        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
            set
            {
                if (value >= 1) pageIndex = value;
            }
        }

        public int PageSize { get; set; }

        public string Sort { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
