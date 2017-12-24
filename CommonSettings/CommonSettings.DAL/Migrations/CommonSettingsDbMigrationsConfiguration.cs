namespace CommonSettings.DAL.Migrations
{
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
            for (int i = 1; i < 20; i++)
            {
                context.Countries.AddOrUpdate(new Country
                {
                    Name = "ÈáÏ " + i,
                    NameEn = "Country" + i,
                    Code = i.ToString("0000"),
                });
            }
        }
    }
}
