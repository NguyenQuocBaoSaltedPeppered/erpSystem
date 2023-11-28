using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ERP.Databases;

namespace ERP.Bases.Models
{
    public interface ICommonModel
    {
    }
    public class CommonModel : BaseModel, ICommonModel
    {
        protected readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CommonModel> _logger;
        private readonly string _className = string.Empty;
        public CommonModel(IServiceProvider provider) : base(provider)
        {
            IWebHostEnvironment webHostEnvironment = provider.GetService<IWebHostEnvironment>();
            ILogger<CommonModel> logger = provider.GetService<ILogger<CommonModel>>();
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _className = GetType().Name;
        }
        protected static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;
        /// <summary>
        /// Hàm tạo từ khoá truy vấn
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        protected string ConvertSearchTerm(string searchTerm)
        {
            return string.IsNullOrEmpty(searchTerm) ? searchTerm : "%" + Helpers.ConvertToUnSign(searchTerm.Trim()).ToLower() + "%";
        }
    }
}