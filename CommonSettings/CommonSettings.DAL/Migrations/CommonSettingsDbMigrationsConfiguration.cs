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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CommonSettings.DAL.CommonSettingDataContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM Common.Regions");
            context.Database.ExecuteSqlCommand("DELETE FROM Common.Countries");

            for (int i = 1; i <= 20; i++)
            {
                context.Countries.AddOrUpdate(c => c.Name,  new Country
                {
                    Name = "الدولة " + i,
                    NameEn = "Country" + i,
                    Code = i.ToString("0000"),
                });
            }

            context.SaveChanges();
            var country = context.Countries.First();
            for (int i = 1; i <= 20; i++)
            {
                context.Regions.AddOrUpdate(c => c.Name, new Region
                {
                    Name = "المنطقة" + i,
                    CountryId = country.Id
                });
            }

            context.SaveChanges();
        }
    }
}
