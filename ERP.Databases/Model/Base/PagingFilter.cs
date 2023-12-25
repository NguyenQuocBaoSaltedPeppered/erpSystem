namespace ERP.Databases
{
    public class PagingFilter
    {
        public PagingFilter()
        {
            CurrentPage = 1;
            PageSize = 50;
        }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        /// <value></value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Số record trên 1 page
        /// </summary>
        /// <value></value>
        public int PageSize { get; set; }
    }
}