namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserProgressLogWithGoalTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProgressLogWithGoals", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.UserProgressLogWithGoals", new[] { "UserProfileId" });
            DropTable("dbo.UserProgressLogWithGoals");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserProgressLogWithGoals", "UserProfileId");
            AddForeignKey("dbo.UserProgressLogWithGoals", "UserProfileId", "dbo.UserProfiles", "Id", cascadeDelete: true);
        }
    }
}
