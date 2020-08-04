namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFkToUserProgressLog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProgressLogs", "UserProfile_Id", "dbo.UserProfiles");
            DropIndex("dbo.UserProgressLogs", new[] { "UserProfile_Id" });
            RenameColumn(table: "dbo.UserProgressLogs", name: "UserProfile_Id", newName: "UserProfileId");
            AlterColumn("dbo.UserProgressLogs", "UserProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserProgressLogs", "UserProfileId");
            AddForeignKey("dbo.UserProgressLogs", "UserProfileId", "dbo.UserProfiles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProgressLogs", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.UserProgressLogs", new[] { "UserProfileId" });
            AlterColumn("dbo.UserProgressLogs", "UserProfileId", c => c.Int());
            RenameColumn(table: "dbo.UserProgressLogs", name: "UserProfileId", newName: "UserProfile_Id");
            CreateIndex("dbo.UserProgressLogs", "UserProfile_Id");
            AddForeignKey("dbo.UserProgressLogs", "UserProfile_Id", "dbo.UserProfiles", "Id");
        }
    }
}
