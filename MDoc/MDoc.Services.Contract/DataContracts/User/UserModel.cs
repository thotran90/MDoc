using System;
using System.ComponentModel.DataAnnotations;

namespace MDoc.Services.Contract.DataContracts.User
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Display(Name = "Login Id")]
        [Required]
        public string LoginId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [Display(Name = "Register Date")]
        public DateTime? RegisterDate { get; set; }

        public bool IsDisabled { get; set; }
        public bool IsUpdate => UserId > 0;
        public bool IsSuperAdmin { get; set; }
    }
}