namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCaseAids : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Cases.CaseAids",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                        AidDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AidDescription = c.String(nullable: false, maxLength: 1000),
                        AidAmount = c.Double(nullable: false),
                        AidType = c.Byte(nullable: false),
                        IsDelivered = c.Boolean(nullable: false),
                        Notes = c.String(maxLength: 1000),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Cases.Cases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Cases.CaseAids", "CaseId", "Cases.Cases");
            DropIndex("Cases.CaseAids", new[] { "CaseId" });
            DropTable("Cases.CaseAids");
        }
    }
}
