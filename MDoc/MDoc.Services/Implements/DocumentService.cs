using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Enums;
using MDoc.Services.Contract.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace MDoc.Services.Implements
{
    public class DocumentService:BaseService,IDocumentService
    {
        #region [Variable]

        private readonly ICustomerService _customerService;
        private readonly IDocumentTypeService _documentTypeService;

        #endregion

        #region [Contructor]
        public DocumentService(IUnitOfWork unitOfWork, ICustomerService customerService, IDocumentTypeService documentTypeService) : base(unitOfWork)
        {
            _customerService = customerService;
            _documentTypeService = documentTypeService;
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
            var result = documents
                .Join(UnitOfWork.GetRepository<Address>().Get(),document=>document.document.ReferenceCountryId,country=>country.AddressId,(document,country)=> new {document=document,country=country})
                .Select(x => new DocumentModel()
            {
                CustomerId = x.document.document.CustomerId,
                Code = x.document.document.Code,
                DocumentId = x.document.document.DocumentId,
                DocumentTypeId = x.document.document.DocumentTypeId.ToString(),
                DocumentStatusId = x.document.document.DocumentStatusId,
                ReferenceCountryId = x.document.document.ReferenceCountryId,
                ReferenceSchoolId = x.document.document.ReferenceSchoolId,
                ReferenceProgramId = x.document.document.ReferenceProgramId,
                FinalSchoolId = x.document.document.FinalSchoolId,
                FinalProgramId = x.document.document.FinalProgramId,
                DocumentType = x.document.document.DocumentType.Label,
                DocumentStatus = x.document.document.DocumentStatus.Label,
                Country = x.country.Label,
                Customer = new CustomerModel()
                {
                    FirstName = x.document.customer.FirstName,
                    LastName = x.document.customer.LastName,
                    CustomerId = x.document.customer.CustomerId
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
                MainResponsibleIds = document.DocumentResponsibles.Where(user=>user.IsMain).Select(x=>x.UserId).JoinStrings(","),
                SubResponsibleIds = document.DocumentResponsibles.Where(user => !user.IsMain).Select(x => x.UserId).JoinStrings(","),
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
            var currentCode = UnitOfWork.GetRepository<Document>().Get().Max(m => m.Code);
            var intCode = Convert.ToInt32(currentCode);
            return (intCode + 1).ToString("D6");
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
                ReferenceSchoolId = model.ReferenceSchoolId,
                DocumentStatusId = (byte) DocumentStatusEnum.PendingForValidation
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
            byte documentTypeId = 0;
            if (byte.TryParse(model.DocumentTypeId, out documentTypeId))
            {
                document.DocumentTypeId = documentTypeId;
            }
            else
            {
                var newDocumentTypeArg = new DocumentTypeModel()
                {
                    Name = model.DocumentTypeId,
                    Description = model.DocumentTypeId
                };
                var newType = _documentTypeService.Create(newDocumentTypeArg);
                document.DocumentTypeId = newType.Id;
            }

            if (!string.IsNullOrEmpty(model.MainResponsibleIds))
            {
                var userIds = model.MainResponsibleIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                foreach (var userId in userIds)
                {
                    document.DocumentResponsibles.Add(new DocumentResponsible()
                    {
                        IsMain = true,
                        UserId = userId
                    });
                }
            }
            if (!string.IsNullOrEmpty(model.SubResponsibleIds))
            {
                var subUserIds = model.SubResponsibleIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                foreach (var userId in subUserIds)
                {
                    document.DocumentResponsibles.Add(new DocumentResponsible()
                    {
                        IsMain = false,
                        UserId = userId
                    });
                }
            }
            UnitOfWork.GetRepository<Document>().Create(document);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Update(DocumentModel model)
        {
            var document = UnitOfWork.GetRepository<Document>().GetByKeys(model.DocumentId);
            if (document == null) return false;
            document.UpdatedById = model.LoggedUserId;
            document.UpdatedDate = DateTime.Now;
            document.ReferenceCountryId = model.ReferenceCountryId;
            document.ReferenceProgramId = model.ReferenceProgramId;
            document.ReferenceSchoolId = model.ReferenceSchoolId;
            document.FinalProgramId = model.FinalProgramId;
            document.FinalSchoolId = model.FinalSchoolId;

            document.DocumentResponsibles.Clear();
            if (!string.IsNullOrEmpty(model.MainResponsibleIds))
            {
                var userIds = model.MainResponsibleIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                foreach (var userId in userIds)
                {
                    document.DocumentResponsibles.Add(new DocumentResponsible()
                    {
                        IsMain = true,
                        UserId = userId
                    });
                }
            }
            if (!string.IsNullOrEmpty(model.SubResponsibleIds))
            {
                var subUserIds = model.SubResponsibleIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                foreach (var userId in subUserIds)
                {
                    document.DocumentResponsibles.Add(new DocumentResponsible()
                    {
                        IsMain = false,
                        UserId = userId
                    });
                }
            }
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Remove(DocumentModel model)
        {
            var document = UnitOfWork.GetRepository<Document>().GetByKeys(model.DocumentId);
            if(document == null) return false;
            document.IsDeleted = true;
            document.UpdatedById = model.LoggedUserId;
            document.UpdatedDate = DateTime.Now;
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateStatus(DocumentModel model)
        {
            var document = UnitOfWork.GetRepository<Document>().GetByKeys(model.DocumentId);
            if(document == null) return false;
            if (model.DocumentStatusId == null) return false;
            document.DocumentStatusId = model.DocumentStatusId.Value;
            document.UpdatedById = model.LoggedUserId;
            document.UpdatedDate = DateTime.Now;
            UnitOfWork.SaveChanges();
            return true;
        }
        #endregion

    }
}