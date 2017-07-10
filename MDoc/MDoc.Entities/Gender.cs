using System.Collections.Generic;

namespace MDoc.Entities
{
    public class Gender
    {
        public byte GenderId { get; set; }
        public string Label { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}