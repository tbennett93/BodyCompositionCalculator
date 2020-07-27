namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGoalIdPropertyToUserProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "Goal_Id", "dbo.Goals");
            DropIndex("dbo.UserProfiles", new[] { "Goal_Id" });
            RenameColumn(table: "dbo.UserProfiles", name: "Goal_Id", newName: "GoalId");
            AlterColumn("dbo.UserProfiles", "GoalId", c => c.Int(nullable: true));
            CreateIndex("dbo.UserProfiles", "GoalId");
            AddForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "GoalId", "dbo.Goals");
            DropIndex("dbo.UserProfiles", new[] { "GoalId" });
            AlterColumn("dbo.UserProfiles", "GoalId", c => c.Int());
            RenameColumn(table: "dbo.UserProfiles", name: "GoalId", newName: "Goal_Id");
            CreateIndex("dbo.UserProfiles", "Goal_Id");
            AddForeignKey("dbo.UserProfiles", "Goal_Id", "dbo.Goals", "Id");
        }
    }
}
