namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFinalWeightAndBodyFatFromGoal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Goals", "FinalWeightInKg");
            DropColumn("dbo.Goals", "FinalBodyFat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goals", "FinalBodyFat", c => c.Int());
            AddColumn("dbo.Goals", "FinalWeightInKg", c => c.Double());
        }
    }
}
