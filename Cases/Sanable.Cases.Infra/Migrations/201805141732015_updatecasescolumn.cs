namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecasescolumn : DbMigration
    {
        public override void Up()
        {
            //AddColumn("Cases.Cases", "bAction", c => c.Boolean(nullable: true, defaultValue: false));
        }
        
        public override void Down()
        {
        }
    }
}
