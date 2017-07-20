using System;

namespace MDoc.Services.Contract.DataContracts
{
    public class SchoolTypeModel
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        public static explicit operator SchoolTypeModel(string v)
        {
            throw new NotImplementedException();
        }
    }
}