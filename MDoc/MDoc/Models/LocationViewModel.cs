using System.ComponentModel.DataAnnotations;
using MDoc.Services.Contract.Enums;

namespace MDoc.Models
{
    public class LocationViewModel
    {
        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [Display(Name = "Province")]
        public int? ProvinceId { get; set; }
        [Display(Name = "District")]
        public int? DistrictId { get; set; }
        [Display(Name = "Ward")]
        public int? WardId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Label { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Address Code")]
        public string AddressCode { get; set; }
        public AddressTypeModel TypeId { get; set; }
    }
}