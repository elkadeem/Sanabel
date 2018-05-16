namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcasecolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("Cases.Cases", "bAction", c => c.Boolean(nullable: true, defaultValue: false));
            //AddColumn("dbo.Blogs", "Rating", c => c.Int(nullable: false, defaultValue: 3));
        }
        
        public override void Down()
        {
        }
    }
}
