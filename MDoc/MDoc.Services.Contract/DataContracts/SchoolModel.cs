using System.ComponentModel.DataAnnotations;

namespace MDoc.Services.Contract.DataContracts
{
    public class SchoolModel
    {
        public int SchoolId { get; set; }
        [Display(Name="Type of school")]
        [Required]
        public string SchoolTypeId { get; set; }
        [Required]
        [Display(Name="Name")]
        [MaxLength(1000,ErrorMessage ="Limited 1000 characters")]
        public string Name { get; set; }
        [Display(Name="Website")]
        public string Website { get; set; }
        [Display(Name = "Contact email")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$" , ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        [Display(Name = "Contact mobile")]
        [RegularExpression(@"^(\d{10,15})$", ErrorMessage = "Only number is allowed.")]
        public string Mobile { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [Display(Name = "Province")]
        public int? ProvinceId { get; set; }
        [Display(Name = "District")]
        public int? DistrictId { get; set; }
        [Display(Name = "Ward")]
        public int? WardId { get; set; }
        public int LoggedUserId { get; set; }
        public string Country { get; set; }
        [Display(Name = "Programs")]
        public string ProgramIds { get; set; }
        [Display(Name = "Education types")]
        public string EducationTypeIds { get; set; }
        public bool IsUpdate => SchoolId > 0;
    }
}