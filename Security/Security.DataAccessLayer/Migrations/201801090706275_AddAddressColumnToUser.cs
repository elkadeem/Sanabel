namespace Security.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressColumnToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("Security.Users", "Address", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("Security.Users", "Address");
        }
    }
}
