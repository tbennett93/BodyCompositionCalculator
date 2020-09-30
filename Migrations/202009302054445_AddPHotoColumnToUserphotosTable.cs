namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPHotoColumnToUserphotosTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserPhotos", "Photo", c => c.Binary(maxLength: 8000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserPhotos", "Photo", c => c.Binary());
        }
    }
}
