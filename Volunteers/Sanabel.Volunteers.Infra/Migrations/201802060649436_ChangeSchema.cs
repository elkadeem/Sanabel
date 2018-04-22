namespace Sanabel.Volunteers.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSchema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "Volunt.Volunteers", newSchema: "Volunteers");
        }
        
        public override void Down()
        {
            MoveTable(name: "Volunteers.Volunteers", newSchema: "Volunt");
        }
    }
}
