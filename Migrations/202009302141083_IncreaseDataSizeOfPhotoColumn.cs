namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseDataSizeOfPhotoColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserPhotos", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserPhotos", "Photo", c => c.Binary(maxLength: 8000));
        }
    }
}
