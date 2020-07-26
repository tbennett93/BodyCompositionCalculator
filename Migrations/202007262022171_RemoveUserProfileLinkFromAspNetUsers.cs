namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserProfileLinkFromAspNetUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfiles");
            DropIndex("dbo.AspNetUsers", new[] { "UserProfileData_Id" });
            DropColumn("dbo.AspNetUsers", "UserProfileData_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserProfileData_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserProfileData_Id");
            AddForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfiles", "Id");
        }
    }
}
