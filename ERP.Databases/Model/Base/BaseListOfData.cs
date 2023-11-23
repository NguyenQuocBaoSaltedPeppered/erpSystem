using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Bases
{
    public class BaseListOfData<TData>
    {
        public BaseListOfData()
        {
            Datas = new List<TData>();
            Paging = new Paging();
        }
        public List<TData> Datas {set; get;}
        public Paging Paging {get; set;}
    }
}