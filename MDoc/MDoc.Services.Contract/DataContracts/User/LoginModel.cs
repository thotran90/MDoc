using System.ComponentModel.DataAnnotations;
using MDoc.Infrastructures;

namespace MDoc.Services.Contract.DataContracts.User
{
    public class LoginModel
    {
        [Display(Name = "username")]
        [Required]
        public string LoginId { get; set; }

        [Display(Name = "password")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Keep me Signed in")]
        public bool IsRemember { get; set; }
        public string SecurePassword => Password.ToMd5();
        public string ReturnUrl { get; set; }
    }
}