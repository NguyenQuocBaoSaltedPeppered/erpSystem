using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases;
using ERP.Bases.Models.User.Schemas;

namespace ERP.Bases.Models
{
    public partial class UserModel : IUserModel //CommonModel, IUserModel
    {
        private readonly ILogger<UserModel> _logger;
        private readonly string _className = string.Empty;
        private readonly DataContext _context;
        // public UserModel(IServiceProvider provider, ILogger<UserModel> logger) : base(provider)
        // {
        //     _logger = logger;
        //     _className = GetType().Name;
        // }
        public UserModel(DataContext context, ILogger<UserModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Whoami? Whoami(int id)
        {
            return _context.Users.Where(u => u.Id == id).Select(u => new Whoami{
                UserId = u.Id,
                UserName = u.Name
            }).FirstOrDefault();
        }
    }
}