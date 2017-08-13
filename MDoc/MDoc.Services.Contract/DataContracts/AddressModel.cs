using System;
using MDoc.Services.Contract.Enums;

namespace MDoc.Services.Contract.DataContracts
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string Label { get; set; }
        public int? ParentId { get; set; }
        public string AddressCode { get; set; }
        public string PostalCode { get; set; }
        public AddressTypeModel Type => (AddressTypeModel)Enum.Parse(typeof(AddressTypeModel), TypeId);
        public string ParentLabel { get; set; }
        public string TypeId { get; set; }
    }
}