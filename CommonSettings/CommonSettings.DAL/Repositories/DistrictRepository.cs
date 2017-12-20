using System.Collections.Generic;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.Repositories;
using BusinessSolutions.Common.EntityFramework;
using BusinessSolutions.Common.Core;
using System.Linq;

namespace CommonSettings.DAL
{
    public class DistrictRepository : BaseEntityFrameworkRepository<int, Place, District>, IDistrictRepository
    {
        public DistrictRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<City> GetDistricts(int regionId, int cityId, string districtName, string code, int pageIndex, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public List<District> GetDistrictsByCityId(int cityId)
        {
            throw new System.NotImplementedException();
        }

        public override District GetDomainEntity(Place entity)
        {
            if (entity == null)
                return null;

            return new District
            {
                Code = entity.Code,
                CityId = entity.ParentPlaceId ?? 0,
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                City = entity.ParentPlace == null ? null : new Domain.Entities.City
                {
                    Code = entity.ParentPlace.Code,
                    Id = entity.ParentPlace.Id,
                    Name = entity.ParentPlace.Name,
                    NameEn = entity.ParentPlace.NameEn,
                    RegionId = entity.ParentPlace.ParentPlaceId ?? 0,
                }
            };
        }

        public override Place GetEntity(District domainEntity)
        {
            var currnetPlace = Set.Local.FirstOrDefault(c => c.Id == domainEntity.Id);
            if (currnetPlace == null)
                currnetPlace = new Place();

            currnetPlace.Name = domainEntity.Name;
            currnetPlace.NameEn = domainEntity.NameEn;
            currnetPlace.Code = domainEntity.Code;
            currnetPlace.CountryId = domainEntity.City.Region.CountryId;
            currnetPlace.ParentPlaceId = domainEntity.CityId;
            currnetPlace.PlaceTypeId = 2;

            return currnetPlace;
        }
    }
}