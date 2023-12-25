using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.AST.Models.Type.Schemas
{
    public class Types
    {
        public int TypeId {get; set;}
        public string TypeName {get; set;}
        public string TypeCode {get; set;}

        public int? ParentId {get; set;}

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


    }
}