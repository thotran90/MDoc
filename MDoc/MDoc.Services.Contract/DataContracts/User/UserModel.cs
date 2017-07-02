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

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [Display(Name = "Register Date")]
        public DateTime? RegisterDate { get; set; }

        public bool IsDisabled { get; set; }
    }
}