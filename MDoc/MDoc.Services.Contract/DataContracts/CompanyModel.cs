namespace MDoc.Services.Contract.DataContracts
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? WardId { get; set; }
        public int? DistrictId { get; set; }
        public string Address { get; set; }
        public string CompanyAdminIds { get; set; }
    }
}