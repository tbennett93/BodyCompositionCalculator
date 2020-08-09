namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionToDataTypesInUserProgressLogWithGoal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProgressLogWithGoals", "GoalWeight", c => c.Double());
            AlterColumn("dbo.UserProgressLogWithGoals", "GoalBodyFat", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProgressLogWithGoals", "GoalBodyFat", c => c.Double());
            AlterColumn("dbo.UserProgressLogWithGoals", "GoalWeight", c => c.Int());
        }
    }
}
