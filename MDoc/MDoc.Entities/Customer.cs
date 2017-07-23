using System;
using System.Collections.Generic;

namespace MDoc.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public byte GenderId { get; set; }
        public DateTime DOB { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string IdentityCardNo { get; set; }
        public DateTime? IdentityCardDateValid { get; set; }
        public DateTime? IdentityCardDateExpired { get; set; }
        public int? IdentityCardPlaceId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int  NationalityId { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}