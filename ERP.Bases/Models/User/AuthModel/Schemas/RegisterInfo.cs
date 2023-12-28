using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Bases.Models.User.Schemas
{
    public class RegisterInfo
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên là trường bắt buộc.")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên là trường bắt buộc.")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email là trường bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu là trường bắt buộc.")]
        public string Password { get; set; }
    }
}
