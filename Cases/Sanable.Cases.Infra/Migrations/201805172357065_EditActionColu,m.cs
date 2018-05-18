namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditActionColum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Cases.Cases", "bAction", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
