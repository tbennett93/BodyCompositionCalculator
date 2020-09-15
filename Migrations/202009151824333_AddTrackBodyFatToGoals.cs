namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrackBodyFatToGoals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goals", "TrackBodyFat", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goals", "TrackBodyFat");
        }
    }
}
