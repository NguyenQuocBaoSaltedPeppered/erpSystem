using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases
{
    public class BaseModel
    {
        protected readonly DataContext _context;
        public BaseModel()
        {
        }

        public BaseModel(
            IServiceProvider provider
        )
        {
            DataContext context = provider.GetService<DataContext>();
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}