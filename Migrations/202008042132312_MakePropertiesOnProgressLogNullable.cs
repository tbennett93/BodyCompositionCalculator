namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePropertiesOnProgressLogNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProgressLogs", "BodyFat", c => c.Int());
            AlterColumn("dbo.UserProgressLogs", "WeightInKgs", c => c.Double());
            DropColumn("dbo.UserProgressLogs", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProgressLogs", "Photo", c => c.Binary());
            AlterColumn("dbo.UserProgressLogs", "WeightInKgs", c => c.Double(nullable: false));
            AlterColumn("dbo.UserProgressLogs", "BodyFat", c => c.Int(nullable: false));
        }
    }
}
