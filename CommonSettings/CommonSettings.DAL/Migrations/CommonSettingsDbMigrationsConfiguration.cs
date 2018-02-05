namespace CommonSettings.DAL.Migrations
{
    using CommonSettings.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class CommonSettingsDbMigrationsConfiguration : DbMigrationsConfiguration<CommonSettings.DAL.CommonSettingDataContext>
    {
        public CommonSettingsDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CommonSettings.DAL.CommonSettingDataContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM Common.Cities");
            context.Database.ExecuteSqlCommand("DELETE FROM Common.Regions");
            context.Database.ExecuteSqlCommand("DELETE FROM Common.Countries");

            var country = new Country()
            {
                Code = "KSA",
                Name = "الممكلة العربية السعودية",
                NameEn = "Saudi Arabia",
            };
            context.Countries.AddOrUpdate(c => c.Name, country);
            context.SaveChanges();
            var region = new Region
            {
                Name = "الرياض",
                NameEn = "Riyadh",
                CountryId = country.Id,
            };
            context.Regions.AddOrUpdate(c => c.Name, region);
            context.SaveChanges();
            var city = new City
            {
                Name = "الرياض",
                RegionId = region.Id,
                NameEn = "Riyadh",
            };

            context.Cities.AddOrUpdate(c => c.Name, city);
            context.SaveChanges();

            var district = new District
            {
                Name = "الازدهار",
                NameEn = "Alizdehar",
                CityId = city.Id,
            };
            context.Districts.AddOrUpdate(c => c.Name, district);

            district = new District
            {
                Name = "المروج",
                NameEn = "Almoruj",
                CityId = city.Id,
            };

            context.Districts.AddOrUpdate(c => c.Name, district);
            context.SaveChanges();

        }
    }
}
