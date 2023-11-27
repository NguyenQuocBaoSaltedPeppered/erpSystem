using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases
{
    /// <summary>
    /// Các giá trị hằng số cơ bản cho các class base
    /// <para>CreatedAt: 16/11/2023</para>
    /// <para>CreatedBy: BaoNQ</para>
    /// </summary>
    public class BaseConstant
    {
        /// <summary>
        /// Thông tin kích thước một trang thông tin cơ bản.
        /// <para>Value: 10</para>
        /// </summary>
        public static readonly int BASE_PAGE_SIZE_10 = 10;
        /// <summary>
        /// Thông tin kích thước một trang thông tin cơ bản.
        /// <para>Value: 50</para>
        /// </summary>
        public static readonly int BASE_PAGE_SIZE_50 = 50;
        /// <summary>
        /// Thông tin kích thước một trang thông tin cơ bản.
        /// <para>Value: 500</para>
        /// </summary>
        public static readonly int BASE_PAGE_SIZE_500 = 500;
    }
}