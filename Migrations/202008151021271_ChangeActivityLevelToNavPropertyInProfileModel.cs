namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeActivityLevelToNavPropertyInProfileModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "ActivityLevelId", c => c.Int(nullable: false));
            Sql("update userprofiles set activitylevelid = 1");
            CreateIndex("dbo.UserProfiles", "ActivityLevelId");
            AddForeignKey("dbo.UserProfiles", "ActivityLevelId", "dbo.ActivityLevels", "Id", cascadeDelete: true);
            DropColumn("dbo.UserProfiles", "ActivityLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "ActivityLevel", c => c.Double(nullable: false));
            DropForeignKey("dbo.UserProfiles", "ActivityLevelId", "dbo.ActivityLevels");
            DropIndex("dbo.UserProfiles", new[] { "ActivityLevelId" });
            DropColumn("dbo.UserProfiles", "ActivityLevelId");
        }
    }
}
