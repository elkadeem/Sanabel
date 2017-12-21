using System.Collections.Generic;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.Repositories;
using BusinessSolutions.Common.EntityFramework;
using BusinessSolutions.Common.Core;
using System.Linq;
using System.Data.Entity;

namespace CommonSettings.DAL

{
    public class DistrictRepository : BaseEntityFrameworkRepository<int, Place, District>, IDistrictRepository
    {
        public DistrictRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<District> GetDistricts(int regionId, int cityId, string districtName
            , string code, int pageIndex, int pageSize)
        {
            var query = Set.AsQueryable();
            if (regionId > 0)
                query = query.Where(c => c.ParentPlace.ParentPlaceId == regionId);
            if (cityId > 0)
                query = query.Where(c => c.ParentPlaceId == cityId);
            if (string.IsNullOrEmpty(districtName))
                query = query.Where(c => c.Name.Contains(districtName)
                 || c.NameEn.Contains(districtName));
            if (string.IsNullOrEmpty(code))
                query = query.Where(c => c.Code.Contains(code));

            int totalCount = query.Count();
            var items = query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(c => GetDomainEntity(c))
                .ToList();

            return new PagedEntity<District>(items, totalCount);
        }

        public List<District> GetDistrictsByCityId(int cityId)
        {
            return Set.Include(c => c.ParentPlace).Where(c => c.ParentPlaceId == cityId)
                .Select(c => GetDomainEntity(c))
                .ToList();
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
            currnetPlace.CountryId = Set.First(c => c.Id == domainEntity.CityId).Id;
            currnetPlace.ParentPlaceId = domainEntity.CityId;
            currnetPlace.PlaceTypeId = 3;

            return currnetPlace;
        }
    }
}