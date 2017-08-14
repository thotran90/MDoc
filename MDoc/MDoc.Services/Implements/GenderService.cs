using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class GenderService : BaseService, IGenderService
    {
        #region [Contructor]

        public GenderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region [Implements]

        public IQueryable<GenderModel> ListOfGenders(string query = "")
            =>
                UnitOfWork.GetRepository<Gender>()
                    .Get(m=> string.IsNullOrEmpty(query) || m.Label.ToLower().Contains(query.ToLower()))
                    .Select(x => new GenderModel
                    {
                        GenderId = x.GenderId,
                        Name = x.Label
                    });

        public GenderModel Single(byte id)
        {
            var gender = UnitOfWork.GetRepository<Gender>().GetByKeys(id);
            if (gender == null) return new GenderModel {GenderId = 0};
            var result = new GenderModel
            {
                Name = gender.Label,
                GenderId = gender.GenderId
            };
            return result;
        }

        #endregion
    }
}