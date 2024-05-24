using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.DTOs
{
    public class AccountDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Chọn một quyền")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Username must be not empty.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(3, ErrorMessage = "Password must be greater than 3 characters")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email must be not empty.")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? Avatar { get; set; }
    }
}
