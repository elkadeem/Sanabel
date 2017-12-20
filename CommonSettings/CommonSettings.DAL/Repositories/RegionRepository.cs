using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.Repositories;
using System.Collections.Generic;
using BusinessSolutions.Common.Core;
using System.Linq;

namespace CommonSettings.DAL
{
    public class RegionRepository : BaseEntityFrameworkRepository<int, Place, Region>, IRegionRepository
    {
        public RegionRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public override Region GetDomainEntity(Place entity)
        {
            if (entity == null)
                return null;

            return new Region
            {
                Code = entity.Code,
                CountryId = entity.CountryId,
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Country = entity.Country == null ? null : new Domain.Entities.Country
                {
                    Code = entity.Country.Code,
                    Id = entity.Country.Id,
                    Name = entity.Country.Name,
                    NameEn = entity.Country.NameEn,
                },
            };
        }

        public override Place GetEntity(Region domainEntity)
        {
            var currnetPlace = Set.Local.FirstOrDefault(c => c.Id == domainEntity.Id);
            if (currnetPlace == null)
                currnetPlace = new Place();

            currnetPlace.Name = domainEntity.Name;
            currnetPlace.NameEn = domainEntity.NameEn;
            currnetPlace.Code = domainEntity.Code;
            currnetPlace.CountryId = domainEntity.CountryId;
            currnetPlace.ParentPlaceId = null;
            currnetPlace.PlaceTypeId = 1;

            return currnetPlace;
        }

        public PagedEntity<Region> GetRegions(int countryId, string regionName, string code, int pageIndex, int pageSize)
        {
            var query = Set.AsQueryable();
            if (countryId > 0)
                query = query.Where(c => c.CountryId == countryId);
            if (!string.IsNullOrEmpty(regionName))
                query = query.Where(c => c.Name.Contains(regionName) || c.NameEn.Contains(regionName));
            if (!string.IsNullOrEmpty(code))
                query = query.Where(c => c.Code.Contains(code));

            int totalCount = query.Count();
            var items = query
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(c => GetDomainEntity(c)).ToList();
            
            return new PagedEntity<Region>(items, totalCount);
        }

        public List<Domain.Entities.Region> GetRegionsByCountryId(int countryId)
        {
            var items = Set.Where(c => c.CountryId == countryId)
                .Select(c => GetDomainEntity(c)).ToList();

            return items;
        }
    }
}