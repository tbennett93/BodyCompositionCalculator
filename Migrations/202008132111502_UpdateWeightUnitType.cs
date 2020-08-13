namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWeightUnitType : DbMigration
    {
        public override void Up()
        {

            DropTable("dbo.WeightUnits");

            CreateTable(
                "dbo.WeightUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WeightUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeightUnit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.WeightUnits");
        }
    }
}
