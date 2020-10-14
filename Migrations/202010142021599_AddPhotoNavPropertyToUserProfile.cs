namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoNavPropertyToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "UserPhotoId", c => c.Int());
            CreateIndex("dbo.UserProfiles", "UserPhotoId");
            AddForeignKey("dbo.UserProfiles", "UserPhotoId", "dbo.UserPhotos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "UserPhotoId", "dbo.UserPhotos");
            DropIndex("dbo.UserProfiles", new[] { "UserPhotoId" });
            DropColumn("dbo.UserProfiles", "UserPhotoId");
        }
    }
}
