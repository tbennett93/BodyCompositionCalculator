namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoTableAndLinkToCheckinLogAsChild : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPhotos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserProgressLogs", "UserPhotoId", c => c.Int());
            CreateIndex("dbo.UserProgressLogs", "UserPhotoId");
            AddForeignKey("dbo.UserProgressLogs", "UserPhotoId", "dbo.UserPhotos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProgressLogs", "UserPhotoId", "dbo.UserPhotos");
            DropIndex("dbo.UserProgressLogs", new[] { "UserPhotoId" });
            DropColumn("dbo.UserProgressLogs", "UserPhotoId");
            DropTable("dbo.UserPhotos");
        }
    }
}
