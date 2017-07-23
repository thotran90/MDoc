namespace MDoc.Services.Contract.DataContracts
{
    public class DocumentModel
    {
        public int DocumentId { get; set; }
        public int CustomerId { get; set; }
        public string DocumentTypeId { get; set; }
        public byte? DocumentStatusId { get; set; }
        public string Code { get; set; }
        public int? ReferenceCountryId { get; set; }
        public int? ReferenceSchoolId { get; set; }
        public int? ReferenceProgramId { get; set; }
        public int? FinalSchoolId { get; set; }
        public int? FinalProgramId { get; set; }
        public int LoggedUserId { get; set; }
        public CustomerModel Customer { get; set; }
        public string DocumentType { get; set; }
        public string DocumentStatus { get; set; }

        public DocumentModel()
        {
            this.Customer = new CustomerModel();
        }
    }
}