namespace Security.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCitiesAndDistricts : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "Common.Cities",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            NameEn = c.String(),
            //            Code = c.String(),
            //            RegionId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Common.Regions", t => t.RegionId, cascadeDelete: true)
            //    .Index(t => t.RegionId);
            
            //CreateTable(
            //    "Common.Districts",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            NameEn = c.String(),
            //            Code = c.String(),
            //            CityId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Common.Cities", t => t.CityId, cascadeDelete: true)
            //    .Index(t => t.CityId);
            
            //CreateTable(
            //    "Common.Regions",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            NameEn = c.String(),
            //            Code = c.String(),
            //            CountryId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("Common.Countries", t => t.CountryId, cascadeDelete: true)
            //    .Index(t => t.CountryId);
            
            //CreateTable(
            //    "Common.Countries",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            NameEn = c.String(),
            //            Code = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            AddColumn("Security.Users", "CityId", c => c.Int(nullable: false));
            AddColumn("Security.Users", "DistrictId", c => c.Int());
            CreateIndex("Security.Users", "CityId");
            CreateIndex("Security.Users", "DistrictId");
            AddForeignKey("Security.Users", "CityId", "Common.Cities", "Id");
            AddForeignKey("Security.Users", "DistrictId", "Common.Districts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Security.Users", "DistrictId", "Common.Districts");
            DropForeignKey("Security.Users", "CityId", "Common.Cities");
            //DropForeignKey("Common.Regions", "CountryId", "Common.Countries");
            //DropForeignKey("Common.Cities", "RegionId", "Common.Regions");
            //DropForeignKey("Common.Districts", "CityId", "Common.Cities");
            //DropIndex("Common.Regions", new[] { "CountryId" });
            //DropIndex("Common.Districts", new[] { "CityId" });
            //DropIndex("Common.Cities", new[] { "RegionId" });
            DropIndex("Security.Users", new[] { "DistrictId" });
            DropIndex("Security.Users", new[] { "CityId" });
            DropColumn("Security.Users", "DistrictId");
            DropColumn("Security.Users", "CityId");
            //DropTable("Common.Countries");
            //DropTable("Common.Regions");
            //DropTable("Common.Districts");
            //DropTable("Common.Cities");
        }
    }
}
