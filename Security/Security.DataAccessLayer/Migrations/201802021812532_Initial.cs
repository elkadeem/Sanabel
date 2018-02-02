namespace Sanabel.Security.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Security.UserClaims",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(nullable: false, maxLength: 200),
                        ClaimValue = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.UserId, t.ClaimType, t.ClaimValue }, unique: true, name: "IX_UserClaim");
            
            CreateTable(
                "Security.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(maxLength: 100),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 200),
                        SecurityStamp = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 20),
                        IsPhoneConfirmed = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LockedOutDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AccessFailedCount = c.Int(nullable: false),
                        EnableTowFactorAuthentication = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "Security.UserLogins",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 100),
                        ProviderKey = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider })
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameAr = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_RoleName");
            
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
            DropForeignKey("Security.UserClaims", "UserId", "Security.Users");
            DropForeignKey("Security.UserRoles", "RoleId", "Security.Roles");
            DropForeignKey("Security.UserRoles", "UserId", "Security.Users");
            DropForeignKey("Security.UserLogins", "UserId", "Security.Users");
            DropIndex("Security.UserRoles", new[] { "RoleId" });
            DropIndex("Security.UserRoles", new[] { "UserId" });
            DropIndex("Security.Roles", "IX_RoleName");
            DropIndex("Security.UserLogins", new[] { "UserId" });
            DropIndex("Security.Users", new[] { "Email" });
            DropIndex("Security.Users", new[] { "UserName" });
            DropIndex("Security.UserClaims", "IX_UserClaim");
            DropTable("Security.UserRoles");
            DropTable("Security.Roles");
            DropTable("Security.UserLogins");
            DropTable("Security.Users");
            DropTable("Security.UserClaims");
        }
    }
}
