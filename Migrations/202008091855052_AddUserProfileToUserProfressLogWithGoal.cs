namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProfileToUserProfressLogWithGoal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProgressLogWithGoals", "UserProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserProgressLogWithGoals", "UserProfileId");
            AddForeignKey("dbo.UserProgressLogWithGoals", "UserProfileId", "dbo.UserProfiles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProgressLogWithGoals", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.UserProgressLogWithGoals", new[] { "UserProfileId" });
            DropColumn("dbo.UserProgressLogWithGoals", "UserProfileId");
        }
    }
}
