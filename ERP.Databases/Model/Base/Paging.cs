using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Bases
{
    /// <summary>
    /// Class chứa đựng thông tin phân trang
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int CurrentPage {set; get;}
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages {set; get;}
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecords {set; get;}
        /// <summary>
        /// Kích thước 1 trang
        /// </summary>
        public int PageSize {set; get;}
        /// <summary>
        /// Hàm dựng không tham số (khởi tạo mặc định)
        /// </summary>
        public Paging()
        {
            CurrentPage = 1;
            PageSize = BaseConstant.BASE_PAGE_SIZE_10;
            TotalPages = 1;
            TotalRecords = 0;
        }
        /// <summary>
        /// Hàm dựng có tham số
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Kích thước 1 trang</param>
        public Paging(int totalRecords, int currentPage, int pageSize = 10)
        {
            TotalRecords = totalRecords;
            PageSize = pageSize;
            if(pageSize == 0)
            {
                // Nếu pageSize bằng 0, lấy hết toàn bộ bản ghi
                PageSize = TotalRecords;
            }
            // Tính toán số lượng trang
            TotalPages = TotalRecords / PageSize + (TotalRecords % PageSize > 0 ? 1 : 0);
            // Kiểm tra trang hiện tại
            if(currentPage > TotalPages)
            {
                // Nếu trang hiện tại lớn hơn tổng số trang và tổng số trang = 0,
                // set trang hiện tại thành 1 nếu không tự động gán thành trang cuối
                currentPage = TotalPages == 0 ? 1 : TotalPages;
            }
            if(currentPage < 1)
            {
                currentPage = 1;
            }
            CurrentPage = currentPage;
        }
    }
}