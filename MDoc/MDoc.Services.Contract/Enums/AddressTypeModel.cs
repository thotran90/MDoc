using System.ComponentModel.DataAnnotations;

namespace MDoc.Services.Contract.Enums
{
    public enum AddressTypeModel
    {
        [Display(Name = "Country")] C,
        [Display(Name = "Province")] P,
        [Display(Name = "Province")] D,
        [Display(Name = "Ward")] W
    }
}