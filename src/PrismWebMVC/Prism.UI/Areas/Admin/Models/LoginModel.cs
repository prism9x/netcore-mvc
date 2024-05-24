using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prism.UI.Areas.Admin.Models
{
    public class LoginModel
    {
        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
        public bool HasRemember { get; set; }
    }
}
