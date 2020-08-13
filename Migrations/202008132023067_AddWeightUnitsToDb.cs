namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeightUnitsToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeightUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeightUnit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WeightUnits");
        }
    }
}
