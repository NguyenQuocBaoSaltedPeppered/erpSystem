namespace ERP.AST.Models.Asset.Schemas
{
    public class AssetUsingHistory
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <value></value>
        public int Id {get; set;}
        /// <summary>
        /// Mã bàn giao
        /// </summary>
        /// <value></value>
        public string Code {get; set;}
        /// <summary>
        /// Ngày bàn giao (ngày tạo)
        /// </summary>
        /// <value></value>
        public DateTime CreatedAt {get; set;}
        /// <summary>
        /// Id người nhận
        /// </summary>
        /// <value></value>
        public int? UserId {get; set;}
        /// <summary>
        /// Mã người nhận
        /// </summary>
        /// <value></value>
        public string UserCode {get; set;}
        /// <summary>
        /// Tên người nhận
        /// </summary>
        /// <value></value>
        public string UserName {get; set;}
        /// <summary>
        /// Chi nhánh nhận
        /// </summary>
        /// <value></value>
        public string UserBranch {get; set;}
        /// <summary>
        /// Bộ phận nhận
        /// </summary>
        /// <value></value>
        public string UserDepartment {get; set;}
        /// <summary>
        /// Số lượng
        /// </summary>
        /// <value></value>
        public int Quantity {get; set;}
    }
}
