namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlaceToCases : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Cases.Cases", "CityId");
            CreateIndex("Cases.Cases", "DistrictId");
            AddForeignKey("Cases.Cases", "CityId", "Common.Cities", "Id");
            AddForeignKey("Cases.Cases", "DistrictId", "Common.Districts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Cases.Cases", "DistrictId", "Common.Districts");
            DropForeignKey("Cases.Cases", "CityId", "Common.Cities");
            DropIndex("Cases.Cases", new[] { "DistrictId" });
            DropIndex("Cases.Cases", new[] { "CityId" });
        }
    }
}
