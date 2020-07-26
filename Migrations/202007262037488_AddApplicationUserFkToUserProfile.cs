namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationUserFkToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserProfiles", "ApplicationUserId");
            AddForeignKey("dbo.UserProfiles", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserProfiles", new[] { "ApplicationUserId" });
            DropColumn("dbo.UserProfiles", "ApplicationUserId");
        }
    }
}
