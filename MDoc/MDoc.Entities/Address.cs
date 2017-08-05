using System.Collections.Generic;

namespace MDoc.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Label { get; set; }
        public string AddressCode { get; set; }
        public string PostalCode { get; set; }
        public bool IsDisabled { get; set; }
        public int? ParentId { get; set; }
        public string TypeId { get; set; }
        public virtual ICollection<Address> Children { get; set; }
        public virtual Address Parent { get; set; }
    }
}