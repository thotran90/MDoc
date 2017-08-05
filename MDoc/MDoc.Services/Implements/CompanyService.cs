using System;
using System.Linq;
using System.Runtime.InteropServices;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace MDoc.Services.Implements
{
    public class CompanyService:BaseService,ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<CompanyModel> ListOfCompanies()
            => UnitOfWork.GetRepository<Company>().Get().Select(m => new CompanyModel()
            {
                Id = m.CompanyId,
                Name = m.Name,
                Address = m.Address,
                Mobile = m.Mobile,
                Email = m.Email,
                Phone = m.Phone,
                Website = m.Website
            });

        public CompanyModel GetCompanyInformation(int companyId)
        {
            var entity = UnitOfWork.GetRepository<Company>().GetByKeys(companyId);
            if(entity == null) return new CompanyModel() {Id = 0};
            var result = new CompanyModel()
            {
                Id = entity.CompanyId,
                Name = entity.Name,
                Address = entity.Address,
                Email = entity.Email,
                CountryId = entity.CountryId,
                Mobile = entity.Mobile,
                DistrictId = entity.DistrictId,
                Website = entity.Website,
                Phone = entity.Phone,
                ProvinceId = entity.ProvinceId,
                WardId = entity.WardId,
                CompanyAdminIds = entity.Administrators.Select(x => x.ApplicationUserId).JoinStrings(",")
            };
            return result;
        }

        public bool Create(CompanyModel model)
        {
            var entity = new Company()
            {
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Mobile = model.Mobile,
                Phone = model.Phone,
                Website = model.Website,
                CountryId = model.CountryId,
                ProvinceId = model.ProvinceId,
                DistrictId = model.DistrictId,
                WardId = model.WardId
            };
            if (!string.IsNullOrEmpty(model.CompanyAdminIds))
            {
                var userIds = model.CompanyAdminIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var users =
                    UnitOfWork.GetRepository<ApplicationUser>().Get(m => userIds.Contains(m.ApplicationUserId)).ToList();
                users.ForEach(x=> entity.Administrators.Add(x));
            }
            UnitOfWork.GetRepository<Company>().Create(entity);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Update(CompanyModel model)
        {
            var entity = UnitOfWork.GetRepository<Company>().GetByKeys(model.Id);
            if(entity == null) return false;
            entity.Name = model.Name;
            entity.Mobile = model.Mobile;
            entity.Email = model.Email;
            entity.Phone = model.Phone;
            entity.CountryId = model.CountryId;
            entity.ProvinceId = model.ProvinceId;
            entity.DistrictId = model.DistrictId;
            entity.WardId = model.WardId;
            entity.Address = model.Address;
            entity.Administrators.Clear();
            if (!string.IsNullOrEmpty(model.CompanyAdminIds))
            {
                var userIds = model.CompanyAdminIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var users =
                    UnitOfWork.GetRepository<ApplicationUser>().Get(m => userIds.Contains(m.ApplicationUserId)).ToList();
                users.ForEach(x => entity.Administrators.Add(x));
            }
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Remove(CompanyModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}