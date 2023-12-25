namespace ERP.AST.Models.Type.Schemas
{
    public class TypeData 
    {
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// <value></value>
        public int? Id {get; set;}
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// <value></value>
        public string Name {get; set;}
        /// <summary>
        /// Id loại tài sản cha
        /// </summary>
        /// <value></value>
        public int? ParentId {get; set;}
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// <value></value>
        public string Code {get; set;}
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// <value></value>
        public int? Level {get; set;}
        /// <summary>
        /// Mục của loại tài sản
        /// </summary>
        /// <value></value>
        public int? Category {get; set;}
        /// <summary>
        /// Tên mục của loại tài sản
        /// </summary>
        /// <value></value>
        public string CategoryName {get; set;}
        /// <summary>
        /// Số lượng tài sản thuộc loại đó
        /// </summary>
        /// <value></value>
        public int? AssetQuantity {get; set;}
    }
}