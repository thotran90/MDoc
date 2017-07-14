using System;
using System.Collections.Generic;

namespace MDoc.Entities
{
    public class School
    {
        public School()
        {
            EducationTypes = new HashSet<EducationType>();
            Programs = new HashSet<Program>();
        }

        public int SchoolId { get; set; }
        public byte SchoolTypeId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual SchoolType SchoolType { get; set; }
        public virtual ICollection<EducationType> EducationTypes { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}