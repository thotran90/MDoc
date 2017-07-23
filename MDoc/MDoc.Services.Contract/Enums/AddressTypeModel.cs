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

    public enum DocumentStatusEnum : byte
    {
        PendingForValidation=1,
        InProgress = 2,
        Canceled = 3,
        Passed = 4,
        Payment = 5,
        Done =6
    }
}