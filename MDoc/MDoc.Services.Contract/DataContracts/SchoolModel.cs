namespace MDoc.Services.Contract.DataContracts
{
    public class SchoolModel
    {
        public int SchoolId { get; set; }
        public byte SchoolTypeId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public int LoggedUserId { get; set; }
    }
}