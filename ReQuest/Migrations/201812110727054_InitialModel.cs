namespace ReQuest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        BuildingLetter = c.String(nullable: false),
                        BuildingDepartment = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.BuildingId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomNumber = c.String(nullable: false, maxLength: 128),
                        RoomCapacity = c.Int(nullable: false),
                        BuildingID = c.Int(),
                    })
                .PrimaryKey(t => t.RoomNumber)
                .ForeignKey("dbo.Buildings", t => t.BuildingID)
                .Index(t => t.BuildingID);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(nullable: false, maxLength: 255),
                        MaterialDescription = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.MaterialId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequestUserID = c.String(),
                        RequestStatus = c.String(),
                        RequestName = c.String(nullable: false, maxLength: 255),
                        RequestDescription = c.String(nullable: false, maxLength: 255),
                        RequestStartTime = c.DateTime(nullable: false),
                        RequestEndTime = c.DateTime(nullable: false),
                        RoomNumber = c.String(maxLength: 128),
                        Calendar_ActivityId = c.Int(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Calendars", t => t.Calendar_ActivityId)
                .ForeignKey("dbo.Rooms", t => t.RoomNumber)
                .Index(t => t.RequestId)
                .Index(t => t.RoomNumber)
                .Index(t => t.Calendar_ActivityId);
            
            CreateTable(
                "dbo.Calendars",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        ActivityUserID = c.String(),
                        ActivityStatus = c.String(),
                        ActivityName = c.String(nullable: false, maxLength: 255),
                        ActivityDescription = c.String(nullable: false, maxLength: 255),
                        ActivityStartTime = c.DateTime(nullable: false),
                        ActivityEndTime = c.DateTime(nullable: false),
                        RoomNumber = c.String(),
                    })
                .PrimaryKey(t => t.ActivityId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MaterialRooms",
                c => new
                    {
                        Material_MaterialId = c.Int(nullable: false),
                        Room_RoomNumber = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Material_MaterialId, t.Room_RoomNumber })
                .ForeignKey("dbo.Materials", t => t.Material_MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_RoomNumber, cascadeDelete: true)
                .Index(t => t.Material_MaterialId)
                .Index(t => t.Room_RoomNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Requests", "RoomNumber", "dbo.Rooms");
            DropForeignKey("dbo.Requests", "Calendar_ActivityId", "dbo.Calendars");
            DropForeignKey("dbo.MaterialRooms", "Room_RoomNumber", "dbo.Rooms");
            DropForeignKey("dbo.MaterialRooms", "Material_MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Rooms", "BuildingID", "dbo.Buildings");
            DropIndex("dbo.MaterialRooms", new[] { "Room_RoomNumber" });
            DropIndex("dbo.MaterialRooms", new[] { "Material_MaterialId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Calendars", new[] { "ActivityId" });
            DropIndex("dbo.Requests", new[] { "Calendar_ActivityId" });
            DropIndex("dbo.Requests", new[] { "RoomNumber" });
            DropIndex("dbo.Requests", new[] { "RequestId" });
            DropIndex("dbo.Materials", new[] { "MaterialId" });
            DropIndex("dbo.Rooms", new[] { "BuildingID" });
            DropTable("dbo.MaterialRooms");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Calendars");
            DropTable("dbo.Requests");
            DropTable("dbo.Materials");
            DropTable("dbo.Rooms");
            DropTable("dbo.Buildings");
        }
    }
}
