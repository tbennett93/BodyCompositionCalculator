namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectWeightColumnNameUserProgressLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProgressLogs", "WeightInKg", c => c.Double());
            DropColumn("dbo.UserProgressLogs", "WeightInKgs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProgressLogs", "WeightInKgs", c => c.Double());
            DropColumn("dbo.UserProgressLogs", "WeightInKg");
        }
    }
}
