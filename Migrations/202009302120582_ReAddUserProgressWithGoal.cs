namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAddUserProgressWithGoal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProgressLogWithGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        WeightInKg = c.Double(),
                        BodyFat = c.Int(),
                        GoalWeight = c.Double(),
                        GoalBodyFat = c.Int(),
                        UserProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProgressLogWithGoals", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.UserProgressLogWithGoals", new[] { "UserProfileId" });
            DropTable("dbo.UserProgressLogWithGoals");
        }
    }
}
