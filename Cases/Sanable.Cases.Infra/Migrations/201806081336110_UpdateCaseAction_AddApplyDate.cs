namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCaseAction_AddApplyDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("Cases.Cases", "CaseSuspensionDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Cases.CaseActions", "StartApplyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("Cases.CaseActions", "StartApplyDate");
            DropColumn("Cases.Cases", "CaseSuspensionDate");
        }
    }
}
