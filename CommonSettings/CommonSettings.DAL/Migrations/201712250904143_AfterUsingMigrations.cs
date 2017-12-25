namespace CommonSettings.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AfterUsingMigrations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Common.Places", "ParentPlaceId", "Common.Places");
            DropForeignKey("Common.Places", "CountryId", "Common.Countries");
            DropIndex("Common.Places", new[] { "CountryId" });
            DropIndex("Common.Places", new[] { "ParentPlaceId" });
            CreateTable(
                "Common.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                        Code = c.String(maxLength: 10),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "Common.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                        Code = c.String(maxLength: 10),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "Common.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                        Code = c.String(maxLength: 10),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            AlterColumn("Common.Countries", "Code", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("Common.Countries", "Name", unique: true, name: "IX_CountryName");
            CreateIndex("Common.Countries", "Code", unique: true, name: "IX_CountryCode");
            DropTable("Common.Places");
        }
        
        public override void Down()
        {
            CreateTable(
                "Common.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                        Code = c.String(maxLength: 10),
                        PlaceTypeId = c.Byte(nullable: false),
                        CountryId = c.Int(nullable: false),
                        ParentPlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("Common.Cities", "RegionId", "Common.Regions");
            DropForeignKey("Common.Regions", "CountryId", "Common.Countries");
            DropForeignKey("Common.Districts", "CityId", "Common.Cities");
            DropIndex("Common.Countries", "IX_CountryCode");
            DropIndex("Common.Countries", "IX_CountryName");
            DropIndex("Common.Regions", new[] { "CountryId" });
            DropIndex("Common.Districts", new[] { "CityId" });
            DropIndex("Common.Cities", new[] { "RegionId" });
            AlterColumn("Common.Countries", "Code", c => c.String(maxLength: 10));
            DropTable("Common.Regions");
            DropTable("Common.Districts");
            DropTable("Common.Cities");
            CreateIndex("Common.Places", "ParentPlaceId");
            CreateIndex("Common.Places", "CountryId");
            AddForeignKey("Common.Places", "CountryId", "Common.Countries", "Id", cascadeDelete: true);
            AddForeignKey("Common.Places", "ParentPlaceId", "Common.Places", "Id");
        }
    }
}
