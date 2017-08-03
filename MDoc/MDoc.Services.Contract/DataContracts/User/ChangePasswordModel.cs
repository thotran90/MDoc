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
        [Required]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm New Password")]
        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}