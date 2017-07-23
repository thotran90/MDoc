using System;
using System.ComponentModel.DataAnnotations;

namespace MDoc.Services.Contract.DataContracts
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Contact email")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Display(Name = "Mobile Number")]
        [Required]
        [RegularExpression(@"^(\d{10,15})$", ErrorMessage = "Only number is allowed.")]
        public string Mobile { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [Display(Name = "Ward")]
        public int WardId { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "Gender")]
        [Required]
        public byte GenderId { get; set; }
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        public DateTime? DOB { get; set; }
        [Display(Name = "Identity Card Place")]
        public int? IdentityCardPlaceId { get; set; }
        [Display(Name = "Identity Card Number")]
        public string IdentityCardNo { get; set; }
        [Display(Name = "Identity Card Valid Date")]
        public DateTime? IdentityCardDateValid { get; set; }
        [Display(Name = "Identity Card Expired Date")]
        public DateTime? IdentityCardDateExpired { get; set; }
        public int LoggedUserId { get; set; }
        [Display(Name = "Nationality")]
        [Required]
        public int NationalityId { get; set; }
    }
}