using System.ComponentModel.DataAnnotations;

namespace MDoc.Services.Contract.DataContracts.User
{
    public class ForgotPasswordModel
    {
        [RegularExpression(
            @"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$",
            ErrorMessage = "Invalid email address")]
        [Display(Name = "Your email")]
        [Required]
        public string Email { get; set; }
    }
}