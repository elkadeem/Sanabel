namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproval : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Cases.CaseApproval",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    CaseId = c.Guid(nullable: false),
                    bApproved = c.Boolean(nullable: true),
                    nApprovedBy = c.Guid(nullable: true),
                    dtApprovalDate = c.DateTime(nullable: true),
                    bRejected = c.Boolean(nullable: true),
                    nRejectedBy = c.Guid(nullable:true),
                    dtRejectionDate = c.DateTime(nullable:true),
                    bSuspended = c.Boolean(nullable: true),
                    nSuspendedBy = c.Guid(nullable: true),
                    dtSuspensionDate = c.DateTime(nullable: true),
                    Comment = c.String(maxLength: 1000),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
        }
    }
}
