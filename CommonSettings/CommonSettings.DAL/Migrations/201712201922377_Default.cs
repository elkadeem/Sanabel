namespace CommonSettings.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Default : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Common.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                        Code = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.Places", t => t.ParentPlaceId)
                .ForeignKey("Common.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId)
                .Index(t => t.ParentPlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Common.Places", "CountryId", "Common.Countries");
            DropForeignKey("Common.Places", "ParentPlaceId", "Common.Places");
            DropIndex("Common.Places", new[] { "ParentPlaceId" });
            DropIndex("Common.Places", new[] { "CountryId" });
            DropTable("Common.Places");
            DropTable("Common.Countries");
        }
    }
}
