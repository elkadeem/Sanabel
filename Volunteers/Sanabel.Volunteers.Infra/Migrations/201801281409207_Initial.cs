namespace Sanabel.Volunteers.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Volunt.Volunteers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 20),
                        CityId = c.Int(nullable: false),
                        DistrictId = c.Int(),
                        HasCar = c.Boolean(nullable: false),
                        Gender = c.Byte(nullable: false),
                        Notes = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.Cities", t => t.CityId)
                .ForeignKey("Common.Districts", t => t.DistrictId)
                .Index(t => t.Name, unique: true, name: "IX_VolunteerName")
                .Index(t => t.Email, unique: true, name: "IX_VolunteerEmail")
                .Index(t => t.Phone, unique: true, name: "IX_VolunteerPhone")
                .Index(t => t.CityId)
                .Index(t => t.DistrictId);
        }
        
        public override void Down()
        {
            DropForeignKey("Volunt.Volunteers", "DistrictId", "Common.Districts");
            DropForeignKey("Volunt.Volunteers", "CityId", "Common.Cities");            
            DropIndex("Volunt.Volunteers", new[] { "DistrictId" });
            DropIndex("Volunt.Volunteers", new[] { "CityId" });
            DropIndex("Volunt.Volunteers", "IX_VolunteerPhone");
            DropIndex("Volunt.Volunteers", "IX_VolunteerEmail");
            DropIndex("Volunt.Volunteers", "IX_VolunteerName");            
            DropTable("Volunt.Volunteers");
        }
    }
}
