using System.ComponentModel.DataAnnotations;

namespace MDoc.Entities.Enums
{
    public enum AddressType
    {
        [Display(Name = "Country")] C,
        [Display(Name = "Province")] P,
        [Display(Name = "District")] D,
        [Display(Name = "Ward")] W
    }
}