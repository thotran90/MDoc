using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Entities.Enums;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace MDoc.Services.Implements
{
    public class SchoolService : BaseService, ISchoolService
    {
        #region [Constructor]

        public SchoolService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion
        
        #region [Variable]

        #endregion

        #region [Implement]

        public IQueryable<SchoolModel> GetSchools(string query)
        {
            var schools = UnitOfWork.GetRepository<School>().Get(m => !m.IsDeleted);
            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                schools = schools.Where(m => m.Name.ToLower().Contains(query));
            }
            var result = schools
                .Join(UnitOfWork.GetRepository<Address>().Get(a => a.TypeId == AddressType.C),
                    school => school.CountryId,
                    country => country.AddressId, (school, country) => new {country, school})
                .Select(x => new SchoolModel
                {
                    SchoolId = x.school.SchoolId,
                    Name = x.school.Name,
                    Email = x.school.Email,
                    Mobile = x.school.Mobile,
                    Website = x.school.Website,
                    CountryId = x.school.CountryId,
                    Country = x.country.Label
                });
            return result;
        }

        public SchoolModel Create(SchoolModel model)
        {
            var entity = new School
            {
                Address = model.Address,
                Name = model.Name,
                CountryId = model.CountryId,
                Email = model.Email,
                Mobile = model.Mobile,
                SchoolTypeId = Convert.ToByte(model.SchoolTypeId),
                DistrictId = model.DistrictId,
                ProvinceId = model.ProvinceId,
                IsDeleted = false,
                CreatedById = model.LoggedUserId,
                CreatedDate = DateTime.Now,
                Description = model.Description,
                WardId = model.WardId,
                Website = model.Website
            };
            if (!string.IsNullOrEmpty(model.EducationTypeIds))
            {
                var educationTypeIds = model.EducationTypeIds.Split(',').Select(x => Convert.ToByte(x)).ToList();
                var educationTypes =
                    UnitOfWork.GetRepository<EducationType>()
                        .Get(m => educationTypeIds.Contains(m.EducationTypeId))
                        .ToList();
                educationTypes.ForEach(type => entity.EducationTypes.Add(type));
            }
            if (!string.IsNullOrEmpty(model.ProgramIds))
            {
                var programIds = model.ProgramIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var programs = UnitOfWork.GetRepository<Program>().Get(m => programIds.Contains(m.ProgramId)).ToList();
                programs.ForEach(program => entity.Programs.Add(program));
            }
            UnitOfWork.GetRepository<School>().Create(entity);
            UnitOfWork.SaveChanges();
            model.SchoolId = entity.SchoolId;
            return model;
        }

        public SchoolModel Update(SchoolModel model)
        {
            var entity = UnitOfWork.GetRepository<School>().GetByKeys(model.SchoolId);
            if(entity == null) return new SchoolModel() {SchoolId = 0};
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.Website = model.Website;
            entity.Mobile = model.Mobile;
            entity.SchoolTypeId = Convert.ToByte(model.SchoolTypeId);
            entity.Address = model.Address;
            entity.CountryId = model.CountryId;
            entity.ProvinceId = model.ProvinceId;
            entity.DistrictId = model.DistrictId;
            entity.WardId = model.WardId;
            entity.Description = model.Description;
            entity.UpdatedById = model.LoggedUserId;
            entity.UpdatedDate = DateTime.Now;
            entity.EducationTypes.Clear();
            if (!string.IsNullOrEmpty(model.EducationTypeIds))
            {
                var educationTypeIds = model.EducationTypeIds.Split(',').Select(x => Convert.ToByte(x)).ToList();
                var educationTypes =
                    UnitOfWork.GetRepository<EducationType>()
                        .Get(m => educationTypeIds.Contains(m.EducationTypeId))
                        .ToList();
                educationTypes.ForEach(type => entity.EducationTypes.Add(type));
            }
            entity.Programs.Clear();
            if (!string.IsNullOrEmpty(model.ProgramIds))
            {
                var programIds = model.ProgramIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var programs = UnitOfWork.GetRepository<Program>().Get(m => programIds.Contains(m.ProgramId)).ToList();
                programs.ForEach(program => entity.Programs.Add(program));
            }
            UnitOfWork.SaveChanges();
            return model;
        }

        public bool Remove(SchoolModel model)
        {
            var entity = UnitOfWork.GetRepository<School>().GetByKeys(model.SchoolId);
            if(entity == null) return false;
            entity.IsDeleted = true;
            entity.UpdatedById = model.LoggedUserId;
            entity.UpdatedDate = DateTime.Now;
            UnitOfWork.SaveChanges();
            return true;
            
        }

        public SchoolModel Detail(int schoolId)
        {
            var entity = UnitOfWork.GetRepository<School>().GetByKeys(schoolId);
            if(entity == null) return  new SchoolModel() {SchoolId =  0};
            var result = new SchoolModel()
            {
                SchoolId = entity.SchoolId,
                Name = entity.Name,
                Address = entity.Address,
                CountryId = entity.CountryId,
                Email = entity.Email,
                Mobile = entity.Mobile,
                Website = entity.Website,
                Description = entity.Description,
                DistrictId = entity.DistrictId,
                ProvinceId = entity.ProvinceId,
                SchoolTypeId = entity.SchoolTypeId.ToString(),
                WardId = entity.WardId,
                ProgramIds = entity.Programs.Select(x => x.ProgramId).JoinStrings(","),
                EducationTypeIds = entity.EducationTypes.Select(x => x.EducationTypeId).JoinStrings(",")
            };
            return result;
        }

        #endregion
    }
}