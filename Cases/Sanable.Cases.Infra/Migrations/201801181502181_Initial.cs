namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "Cases.CaseReseraches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                        ResearchDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ResearchType = c.Byte(nullable: false),
                        OtherResearchType = c.String(maxLength: 50),
                        JobName = c.String(nullable: false, maxLength: 100),
                        QualificationName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 200),
                        EgyptRegionId = c.Int(),
                        MembershipNo = c.String(maxLength: 20),
                        MembershipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsMemberInOtherAssociation = c.Boolean(nullable: false),
                        OtherAssociationName = c.String(maxLength: 50),
                        JobType = c.Byte(nullable: false),
                        LeaveWorkReason = c.String(maxLength: 100),
                        Income = c.Double(nullable: false),
                        NoIncomeReason = c.String(maxLength: 200),
                        NumberOfFollowers = c.Int(nullable: false),
                        HasClosedRelatives = c.Boolean(nullable: false),
                        ClosedRelativeName = c.String(maxLength: 100),
                        ClosedRelativePhone = c.String(maxLength: 20),
                        YearlyHouseRent = c.Double(nullable: false),
                        IsHouseRentPaid = c.Boolean(nullable: false),
                        TransportationType = c.Byte(nullable: false),
                        Description = c.String(maxLength: 500),
                        Skils = c.String(maxLength: 500),
                        NeededHelp = c.String(maxLength: 500),
                        HealthStatus = c.Byte(nullable: false),
                        HealthStatusDescription = c.String(),
                        HealthNotes = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Cases.Cases", t => t.CaseId)
                .Index(t => t.CaseId);
            
            CreateTable(
                "Cases.Cases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        CityId = c.Int(nullable: false),
                        DistrictId = c.Int(),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Gender = c.Byte(nullable: false),
                        CaseType = c.Byte(nullable: false),
                        Address = c.String(maxLength: 200),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_CaseName")
                .Index(t => t.Phone, unique: true, name: "IX_CasePhone");
            
            CreateTable(
                "Cases.CaseFollowers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CaseResearchId = c.Guid(nullable: false),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Education = c.String(),
                        JobName = c.String(),
                        InCome = c.Double(nullable: false),
                        Notes = c.String(),
                        HealthStatus = c.Byte(nullable: false),
                        HealthStatusDescription = c.String(),
                        HealthNotes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Cases.CaseReseraches", t => t.CaseResearchId, cascadeDelete: true)
                .Index(t => t.CaseResearchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Cases.CaseFollowers", "CaseResearchId", "Cases.CaseReseraches");
            DropForeignKey("Cases.CaseReseraches", "CaseId", "Cases.Cases");
            DropIndex("Cases.CaseFollowers", new[] { "CaseResearchId" });
            DropIndex("Cases.Cases", "IX_CasePhone");
            DropIndex("Cases.Cases", "IX_CaseName");
            DropIndex("Cases.CaseReseraches", new[] { "CaseId" });
            DropTable("Cases.CaseFollowers");
            DropTable("Cases.Cases");
            DropTable("Cases.CaseReseraches");
        }
    }
}
