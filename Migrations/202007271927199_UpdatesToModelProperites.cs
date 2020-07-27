namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesToModelProperites : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals");
            DropIndex("dbo.UserProfiles", new[] { "GoalId" });
            AlterColumn("dbo.UserProfiles", "GoalId", c => c.Int());
            CreateIndex("dbo.UserProfiles", "GoalId");
            AddForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals");
            DropIndex("dbo.UserProfiles", new[] { "GoalId" });
            AlterColumn("dbo.UserProfiles", "GoalId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserProfiles", "GoalId");
            AddForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals", "Id", cascadeDelete: true);
        }
    }
}
