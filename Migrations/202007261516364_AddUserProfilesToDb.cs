namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProfilesToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        HeightInCm = c.Int(nullable: false),
                        Bmr = c.Double(nullable: false),
                        CurrentBf = c.Int(nullable: false),
                        TargetBf = c.Int(nullable: false),
                        ActivityLevel = c.Double(nullable: false),
                        WeightUnit = c.String(),
                        HeightUnit = c.String(),
                        Goal_Id = c.Int(),
                        Macros_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goals", t => t.Goal_Id)
                .ForeignKey("dbo.Macros", t => t.Macros_Id)
                .Index(t => t.Goal_Id)
                .Index(t => t.Macros_Id);
            
            CreateTable(
                "dbo.Macros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Protein = c.Int(nullable: false),
                        Fat = c.Int(nullable: false),
                        Carbs = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProgressLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BodyFat = c.Int(nullable: false),
                        WeightInKgs = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Photo = c.Binary(),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProgressLogs", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfiles", "Macros_Id", "dbo.Macros");
            DropForeignKey("dbo.UserProfiles", "Goal_Id", "dbo.Goals");
            DropIndex("dbo.UserProgressLogs", new[] { "UserProfile_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Macros_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Goal_Id" });
            DropTable("dbo.UserProgressLogs");
            DropTable("dbo.Macros");
            DropTable("dbo.UserProfiles");
        }
    }
}
