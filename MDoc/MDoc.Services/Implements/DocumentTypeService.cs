using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class DocumentTypeService : BaseService, IDocumentTypeService
    {
        #region [Contructor]

        public DocumentTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region [Implements]

        public IQueryable<DocumentTypeModel> ListOfDocumentType(string query = "")
            => UnitOfWork.GetRepository<DocumentType>().Get(m => !m.IsDisabled)
                .Where(m => m.Label.ToLower().Contains(query.ToLower()))
                .Select(x => new DocumentTypeModel
                {
                    Id = x.DocumentTypeId,
                    Name = x.Label
                });

        public DocumentTypeModel Single(byte id)
        {
            var entity = UnitOfWork.GetRepository<DocumentType>().GetByKeys(id);
            if (entity == null) return new DocumentTypeModel {Id = 0};
            var result = new DocumentTypeModel
            {
                Name = entity.Label,
                Id = entity.DocumentTypeId,
                Description = entity.Description
            };
            return result;
        }

        public DocumentTypeModel Create(DocumentTypeModel model)
        {
            var entity = new DocumentType
            {
                Label = model.Name,
                IsDisabled = false,
                Description = model.Description
            };
            UnitOfWork.GetRepository<DocumentType>().Create(entity);
            UnitOfWork.SaveChanges();
            model.Id = entity.DocumentTypeId;
            return model;
        }

        #endregion
    }
}