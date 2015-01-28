namespace Floreview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Avatar = c.String(),
                        Title = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.BlogCategories", t => t.Category_ID)
                .Index(t => t.Author_Id)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        AccessCode = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false),
                        DescriptionShort = c.String(nullable: false, maxLength: 150),
                        DescriptionLong = c.String(nullable: false, maxLength: 500),
                        Coordinates = c.Geography(),
                        Avatar = c.String(),
                        ImageList = c.String(),
                        Website = c.String(),
                        Email = c.String(nullable: false),
                        Facebook = c.String(nullable: false),
                        Florist_ID = c.Int(),
                        Genre_ID = c.Int(),
                        Location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Florists", t => t.Florist_ID)
                .ForeignKey("dbo.Genres", t => t.Genre_ID)
                .ForeignKey("dbo.Locations", t => t.Location_ID)
                .Index(t => t.Florist_ID)
                .Index(t => t.Genre_ID)
                .Index(t => t.Location_ID);
            
            CreateTable(
                "dbo.Florists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 35),
                        LastName = c.String(nullable: false, maxLength: 35),
                        Phone = c.String(),
                        Cellphone = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Zip = c.Int(nullable: false),
                        City = c.String(nullable: false),
                        ZipMain = c.Int(nullable: false),
                        CityMain = c.String(),
                        Province_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Provinces", t => t.Province_ID)
                .Index(t => t.Province_ID);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanyLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Company_ID = c.Int(),
                        Location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .ForeignKey("dbo.Locations", t => t.Location_ID)
                .Index(t => t.Company_ID)
                .Index(t => t.Location_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyLocations", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.CompanyLocations", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.Companies", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Province_ID", "dbo.Provinces");
            DropForeignKey("dbo.Companies", "Genre_ID", "dbo.Genres");
            DropForeignKey("dbo.Companies", "Florist_ID", "dbo.Florists");
            DropForeignKey("dbo.Blogs", "Category_ID", "dbo.BlogCategories");
            DropForeignKey("dbo.Blogs", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CompanyLocations", new[] { "Location_ID" });
            DropIndex("dbo.CompanyLocations", new[] { "Company_ID" });
            DropIndex("dbo.Companies", new[] { "Location_ID" });
            DropIndex("dbo.Locations", new[] { "Province_ID" });
            DropIndex("dbo.Companies", new[] { "Genre_ID" });
            DropIndex("dbo.Companies", new[] { "Florist_ID" });
            DropIndex("dbo.Blogs", new[] { "Category_ID" });
            DropIndex("dbo.Blogs", new[] { "Author_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropTable("dbo.CompanyLocations");
            DropTable("dbo.Provinces");
            DropTable("dbo.Locations");
            DropTable("dbo.Genres");
            DropTable("dbo.Florists");
            DropTable("dbo.Companies");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Blogs");
        }
    }
}
