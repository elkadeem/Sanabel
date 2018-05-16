namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcasecolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("Cases.Cases", "bApproved", c => c.Boolean(nullable: true));
            AddColumn("Cases.Cases", "bRejected", c => c.Boolean(nullable: true));
            AddColumn("Cases.Cases", "bSuspended", c => c.Boolean(nullable: true));

            AddColumn("Cases.Cases", "dtApprovalDate", c => c.DateTime(nullable: true));
            AddColumn("Cases.Cases", "dtRejectionDate", c => c.DateTime(nullable: true));
            AddColumn("Cases.Cases", "dtSuspensionDate", c => c.DateTime(nullable: true));

            AddColumn("Cases.Cases", "nApprovedBy", c => c.Guid(nullable: true));
            AddColumn("Cases.Cases", "nRejectedBy", c => c.DateTime(nullable: true));
            AddColumn("Cases.Cases", "nSuspendedBy", c => c.DateTime(nullable: true));

            AddColumn("Cases.Cases", "Comment", c => c.String(nullable: true));

        }
        
        public override void Down()
        {
            DropColumn("Cases.Cases", "bAction");
        }
    }
}
