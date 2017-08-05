using System.Collections.Generic;

namespace MDoc.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardId { get; set; }
        public string Address { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<ApplicationUser> Administrators { get; set; }
    }
}