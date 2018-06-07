namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Cases.CaseActions",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    CaseActionDate = c.DateTime(nullable: false),
                    CaseSuspensionDate = c.DateTime(nullable: false),
                    OldStatus = c.Byte(nullable: false),
                    Status = c.Byte(nullable: false),
                    Comment = c.String(),
                    CreatedBy = c.String(),
                    CaseId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                ;
            
            AddColumn("Cases.Cases", "CaseStatus", c => c.Byte(nullable: false));
            DropColumn("Cases.Cases", "bAction");
            DropColumn("Cases.Cases", "Comment");
            DropColumn("Cases.Cases", "bApproved");
            DropColumn("Cases.Cases", "nApprovedBy");
            DropColumn("Cases.Cases", "dtApprovalDate");
            DropColumn("Cases.Cases", "bRejected");
            DropColumn("Cases.Cases", "nRejectedBy");
            DropColumn("Cases.Cases", "dtRejectionDate");
            DropColumn("Cases.Cases", "bSuspended");
            DropColumn("Cases.Cases", "nSuspendedBy");
            DropColumn("Cases.Cases", "dtSuspensionDate");
            DropColumn("Cases.Cases", "sAction");
        }
        
        public override void Down()
        {
            
        }
    }
}
