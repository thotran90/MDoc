using System.ComponentModel.DataAnnotations;
using MDoc.Infrastructures;

namespace MDoc.Services.Contract.DataContracts.User
{
    public class LoginModel
    {
        [Display(Name = "Login Id")]
        [Required]
        public string LoginId { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        public string SecurePassword => Password.ToMd5();
    }
}