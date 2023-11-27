using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases
{
    public class StartupState
    {
        public static StartupState Instance { get; protected set; } = new StartupState();
        private IServiceProvider _services;
        public virtual IServiceProvider Services
        {
            get
            {
                lock (_services)
                {
                    return _services;
                }
            }
            set
            {
                if (_services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                _services = value;
            }
        }
    }
}