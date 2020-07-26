namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGoalToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartWeightInKg = c.Double(nullable: false),
                        TargetWeightInKg = c.Double(nullable: false),
                        FinalWeightInKg = c.Double(),
                        StartBodyFat = c.Int(),
                        TargetBodyFat = c.Int(),
                        FinalBodyFat = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Goals");
        }
    }
}
