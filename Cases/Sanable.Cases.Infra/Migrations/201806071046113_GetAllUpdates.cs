namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetAllUpdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Cases.Cases", "Description", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("Cases.Cases", "Description", c => c.String(maxLength: 1000));
        }
    }
}
