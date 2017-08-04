using System.ComponentModel.DataAnnotations;

namespace MDoc.Services.Contract.DataContracts.User
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        [Display(Name = "Current Password")]
        [Required]
        public string OldPassword { get; set; }
        [Display(Name = "New Password")]
        [StringLength(500, ErrorMessage = "Must be between 8 and 500 characters", MinimumLength = 8)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])[^\\s]{8,}$",ErrorMessage = "Minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number")]
        [Required]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm New Password")]
        [Required]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}