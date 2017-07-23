using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class DocumentService:BaseService,IDocumentService
    {
        #region [Variable]

        private readonly ICustomerService _customerService;

        #endregion
        #region [Contructor]
        public DocumentService(IUnitOfWork unitOfWork, ICustomerService customerService) : base(unitOfWork)
        {
            _customerService = customerService;
        }

        #endregion

        #region [Implements]

        public IQueryable<DocumentModel> ListOfDocument(string query = "")
        {
            var documents = UnitOfWork.GetRepository<Document>().Get(m => !m.IsDeleted)
                .Join(UnitOfWork.GetRepository<Customer>().Get(), document => document.CustomerId,
                    customer => customer.CustomerId,
                    (document, customer) => new {customer = customer, document = document});
            var documentId = 0;
            if (int.TryParse(query, out documentId))
            {
                documents = documents.Where(m => int.Parse(m.document.Code) == documentId);
            }
            else
            {
                query = query.ToLower();
                documents =
                    documents.Where(
                        m =>
                            m.customer.FirstName.ToLower().Contains(query) ||
                            m.customer.LastName.ToLower().Contains(query) ||
                            (m.customer.LastName + m.customer.FirstName).ToLower().Contains(query) ||
                            (m.customer.FirstName + m.customer.LastName).ToLower().Contains(query));
            }
            var result = documents.Select(x => new DocumentModel()
            {
                CustomerId = x.document.CustomerId,
                Code = x.document.Code,
                DocumentId = x.document.DocumentId,
                DocumentTypeId = x.document.DocumentTypeId.ToString(),
                DocumentStatusId = x.document.DocumentStatusId,
                ReferenceCountryId = x.document.ReferenceCountryId,
                ReferenceSchoolId = x.document.ReferenceSchoolId,
                ReferenceProgramId = x.document.ReferenceProgramId,
                FinalSchoolId = x.document.FinalSchoolId,
                FinalProgramId = x.document.FinalProgramId,
                Customer = new CustomerModel()
                {
                    FirstName = x.customer.FirstName,
                    LastName = x.customer.LastName,
                    CustomerId = x.customer.CustomerId
                }
            });
            return result;
        }


        public DocumentModel Single(int documentId)
        {
            var document = UnitOfWork.GetRepository<Document>().GetByKeys(documentId);
            if(document == null) return new DocumentModel() {DocumentId = 0};
            var customer = UnitOfWork.GetRepository<Customer>().GetByKeys(document.CustomerId);
            var result = new DocumentModel()
            {
                CustomerId = document.CustomerId,
                Code = document.Code,
                DocumentId = document.DocumentId,
                DocumentTypeId = document.DocumentTypeId.ToString(),
                DocumentStatusId = document.DocumentStatusId,
                ReferenceCountryId = document.ReferenceCountryId,
                ReferenceSchoolId = document.ReferenceSchoolId,
                ReferenceProgramId = document.ReferenceProgramId,
                FinalSchoolId = document.FinalSchoolId,
                FinalProgramId = document.FinalProgramId,
                Customer = new CustomerModel()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    CustomerId = customer.CustomerId,
                    Address = customer.Address,
                    CountryId = customer.CountryId,
                    ProvinceId = customer.ProvinceId,
                    DistrictId = customer.DistrictId,
                    WardId = customer.WardId,
                    DOB = customer.DOB,
                    Email = customer.Email,
                    Mobile = customer.Mobile,
                    NationalityId = customer.NationalityId,
                    GenderId = customer.GenderId,
                    IdentityCardNo = customer.IdentityCardNo,
                    IdentityCardDateExpired = customer.IdentityCardDateExpired,
                    IdentityCardDateValid = customer.IdentityCardDateValid,
                    IdentityCardPlaceId = customer.IdentityCardPlaceId
                }
            };
            return result;
        }

        public string GenerateDocumentCode()
        {
            if (!UnitOfWork.GetRepository<Document>().Get().Any()) return 1.ToString("D6");
            var currentCode = UnitOfWork.GetRepository<Document>().Get().Max(m => Convert.ToInt32(m));
            return (currentCode + 1).ToString("D6");
        }

        public bool Create(DocumentModel model)
        {
            var document = new Document()
            {
                Code = GenerateDocumentCode(),
                CreatedById = model.LoggedUserId,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                FinalProgramId = model.FinalProgramId,
                FinalSchoolId = model.FinalSchoolId,
                ReferenceCountryId = model.ReferenceCountryId,
                ReferenceProgramId = model.ReferenceProgramId,
                ReferenceSchoolId = model.ReferenceSchoolId
            };
            if (model.CustomerId == 0)
            {
                var newCustomer = _customerService.Create(model.Customer);
                document.CustomerId = newCustomer.CustomerId;
            }
            else
            {
                document.CustomerId = model.CustomerId;
            }

            return true;
        }

        public bool Update(DocumentModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(DocumentModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateStatus(DocumentModel model)
        {
            throw new System.NotImplementedException();
        }
        #endregion

    }
}