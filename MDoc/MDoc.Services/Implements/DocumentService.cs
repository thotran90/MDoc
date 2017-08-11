using System;
using System.Data.SqlClient;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.DataContracts.User;
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

        public bool CanEditDocument(int userId, int documentId)
            =>
                UnitOfWork.GetRepository<Document>()
                    .Get()
                    .Any(
                        m =>
                            m.DocumentId == documentId &&
                            (m.DocumentResponsibles.Any(res => res.UserId == userId) || m.CreatedById == userId));

        public IQueryable<DocumentModel> ListOfDocument(ListDocumentArgument argument)
        {
            var documents = (from doc in UnitOfWork.GetRepository<Document>().Get()
                join cus in UnitOfWork.GetRepository<Customer>().Get() on doc.CustomerId equals cus.CustomerId
                join creator in UnitOfWork.GetRepository<ApplicationUser>().Get() on doc.CreatedById equals
                    creator.ApplicationUserId
                join country in UnitOfWork.GetRepository<Address>().Get(m => m.TypeId == "C") on doc.ReferenceCountryId
                    equals country.AddressId
                let isAdmin = argument.IsAdmin
                where
                    isAdmin ||
                    (doc.CreatedById == argument.UserId ||
                     doc.DocumentResponsibles.Any(res => res.UserId == argument.UserId))
                select new DocumentModel()
                {
                    CustomerId = doc.CustomerId,
                    Code = doc.Code,
                    DocumentId = doc.DocumentId,
                    DocumentTypeId = doc.DocumentTypeId.ToString(),
                    DocumentType = doc.DocumentType.Label,
                    CreatedDate = doc.CreatedDate,
                    Creator = creator.UserName,
                    Country = country.Label,
                    DocumentStatus = doc.DocumentStatus.Label,
                    DocumentStatusId = doc.DocumentStatusId,
                    IsCreatedContract = doc.IsCreatedContract,
                    IsNeedContract = doc.IsNeedContract,
                    Customer =  new CustomerModel()
                    {
                        FirstName = cus.FirstName,
                        LastName = cus.LastName,
                        CustomerId = cus.CustomerId
                    },
                    ResponsibleUsers = doc.DocumentResponsibles.Select(x=> new UserModel()
                    {
                        UserName = x.User.UserName,
                        UserId = x.UserId
                    }).ToList()
                });
            return documents;
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
                IsCreatedContract = document.IsCreatedContract,
                IsNeedContract = document.IsNeedContract,
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
                IsNeedContract = model.IsNeedContract,
                IsCreatedContract = model.IsCreatedContract,
                ReferenceSchoolId = model.ReferenceSchoolId,
                DocumentStatusId = (byte) DocumentStatusEnum.New
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
            if (!document.DocumentResponsibles.Any())
            {
                document.DocumentResponsibles.Add(new DocumentResponsible()
                {
                    IsMain = true,
                    UserId = model.LoggedUserId
                });
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
            document.IsNeedContract = model.IsNeedContract;
            document.IsCreatedContract = model.IsCreatedContract;
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

        public bool SaveChecklist(DocumentChecklistModel model)
        {
            var item = UnitOfWork.GetRepository<DocumentChecklist>()
                .GetByKeys(model.DocumentId, model.ChecklistId);
            if (item == null)
            {
                var newItem = new DocumentChecklist()
                {
                    DocumentId = model.DocumentId,
                    ChecklistId = model.ChecklistId,
                    IsChecked = model.IsChecked,
                    UpdatedById = model.LoggedUserId,
                    UpdatedDate = DateTime.Now
                };
                UnitOfWork.GetRepository<DocumentChecklist>().Create(newItem);
                UnitOfWork.SaveChanges();
                return true;
            }
            else
            {
                item.UpdatedById = model.LoggedUserId;
                item.UpdatedDate = DateTime.Now;
                item.IsChecked = model.IsChecked;
            }
            UnitOfWork.SaveChanges();
            return true;
        }

        #endregion

    }
}