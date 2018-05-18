namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActionColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Cases.Cases", "bAction", c => c.Boolean(nullable: true, defaultValue: false));
            AddColumn("Cases.Cases", "sAction", c => c.String());
            
        }
        
        public override void Down()
        {
            
           
        }
    }
}
