using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    /// <summary>
    /// Các dữ liệu số lượng liên quan đến tài sản
    /// </summary>
    public interface IAssetQuantityColumn
    {
        /// <summary>
        /// Số lượng tài sản
        /// <para> = Số lượng Allocated + Remain</para>
        /// </summary>
        /// <value></value>
        public int Quantity {get; set;}
        /// <summary>
        /// Số lượng tài sản cấp phát
        /// </summary>
        /// <value></value>
        public int QuantityAllocated {get; set;}
        /// <summary>
        /// Số lượng tài sản còn lại
        /// </summary>
        /// <value></value>
        public int QuantityRemain {get; set;}
        /// <summary>
        /// Số lượng tài sản mất
        /// </summary>
        /// <value></value>
        public int QuantityBroken {get; set;}
        /// <summary>
        /// Số lượng tài sản báo huỷ
        /// </summary>
        /// <value></value>
        public int QuantityCancel {get; set;}
        /// <summary>
        /// Số lượng tài sản đang được bảo hành
        /// </summary>
        /// <value></value>
        public int QuantityGuarantee {get; set;}
        /// <summary>
        /// Số lượng tài sản bị báo mất
        /// </summary>
        /// <value></value>
        public int QuantityLost {get; set;}
    }
}