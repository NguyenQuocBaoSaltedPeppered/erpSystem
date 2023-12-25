using System;
namespace ERP.AST.Models.Unit.Schemas
{
    public class UnitCreateSchema
    {
        public int? Id { get; set; }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// Mã đơn vị tính
        /// </summary>
        /// <value></value>
        public string Code { get; set; }
    }
}