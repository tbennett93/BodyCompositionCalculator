namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveGoalForeignKeyFromParentToChild : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals");
            DropIndex("dbo.UserProfiles", new[] { "GoalId" });
            AddColumn("dbo.Goals", "UserProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Goals", "UserProfileId");
            AddForeignKey("dbo.Goals", "UserProfileId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            DropColumn("dbo.UserProfiles", "GoalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "GoalId", c => c.Int());
            DropForeignKey("dbo.Goals", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.Goals", new[] { "UserProfileId" });
            DropColumn("dbo.Goals", "UserProfileId");
            CreateIndex("dbo.UserProfiles", "GoalId");
            AddForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals", "Id");
        }
    }
}
