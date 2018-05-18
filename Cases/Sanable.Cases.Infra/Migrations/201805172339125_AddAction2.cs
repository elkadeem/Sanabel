namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAction2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Cases.Cases", "bAction", c => c.Boolean(nullable: true, defaultValue: false));
        }
        
        public override void Down()
        {
        }
    }
}
