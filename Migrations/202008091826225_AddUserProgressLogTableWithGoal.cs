namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProgressLogTableWithGoal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProgressLogWithGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        WeightInKg = c.Double(),
                        BodyFat = c.Int(),
                        GoalWeight = c.Int(),
                        GoalBodyFat = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProgressLogWithGoals");
        }
    }
}
