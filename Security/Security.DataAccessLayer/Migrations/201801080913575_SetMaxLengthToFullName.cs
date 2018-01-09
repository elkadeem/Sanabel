namespace Security.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetMaxLengthToFullName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Security.Users", "FullName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("Security.Users", "FullName", c => c.String());
        }
    }
}
