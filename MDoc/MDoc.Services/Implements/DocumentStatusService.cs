using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class DocumentStatusService : BaseService, IDocumentStatusService
    {
        #region [Contructor]

        public DocumentStatusService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region [Implements]

        public IQueryable<DocumentStatusModel> ListOfDocumentStatus(string query = "")
            =>
                UnitOfWork.GetRepository<DocumentStatus>()
                    .Get(m => m.Label.ToLower().Contains(query.ToLower()))
                    .Select(x => new DocumentStatusModel()
                    {
                        Id = x.DocumentStatusId,
                        Name = x.Label
                    });

        public bool Create(DocumentStatusModel model)
        {
            var entity = new DocumentStatus()
            {
                Label = model.Name,
                IsDisabled = false
            };
            UnitOfWork.GetRepository<DocumentStatus>().Create(entity);
            UnitOfWork.SaveChanges();
            return entity.DocumentStatusId > 0;
        }

        #endregion
    }
}