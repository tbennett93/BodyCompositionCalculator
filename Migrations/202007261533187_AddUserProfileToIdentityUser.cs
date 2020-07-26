namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProfileToIdentityUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserProfileData_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserProfileData_Id");
            AddForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfiles");
            DropIndex("dbo.AspNetUsers", new[] { "UserProfileData_Id" });
            DropColumn("dbo.AspNetUsers", "UserProfileData_Id");
        }
    }
}
