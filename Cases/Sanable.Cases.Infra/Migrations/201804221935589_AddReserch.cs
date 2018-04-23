namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReserch : DbMigration
    {
        public override void Up()
        {
            AddColumn("Cases.CaseReseraches", "CaseResearchRequest", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Cases.CaseReseraches", "CaseResearchRequest");
        }
    }
}
