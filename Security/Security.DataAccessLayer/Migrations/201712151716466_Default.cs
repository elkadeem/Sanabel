namespace Security.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Default : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Security.Roles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        RoleName = c.String(nullable: false, maxLength: 100),
                        RoleNameAr = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "Security.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 200),
                        SecurityStamp = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 20),
                        IsPhoneConfirmed = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LockedOutDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AccessFailedCount = c.Int(nullable: false),
                        EnableTowFactorAuthentication = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "Security.UserClaims",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(nullable: false, maxLength: 200),
                        ClaimValue = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.UserLogins",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 100),
                        ProviderKey = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider })
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.UserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("Security.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.UserLogins", "UserId", "Security.Users");
            DropForeignKey("Security.UserRoles", "RoleId", "Security.Roles");
            DropForeignKey("Security.UserRoles", "UserId", "Security.Users");
            DropForeignKey("Security.UserClaims", "UserId", "Security.Users");
            DropIndex("Security.UserRoles", new[] { "RoleId" });
            DropIndex("Security.UserRoles", new[] { "UserId" });
            DropIndex("Security.UserLogins", new[] { "UserId" });
            DropIndex("Security.UserClaims", new[] { "UserId" });
            DropTable("Security.UserRoles");
            DropTable("Security.UserLogins");
            DropTable("Security.UserClaims");
            DropTable("Security.Users");
            DropTable("Security.Roles");
        }
    }
}
