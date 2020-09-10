namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCalclationBasisMandatory : DbMigration
    {
        public override void Up()
        {
            Sql("update goals set calculationbasis = 'Weight'");
            AlterColumn("dbo.Goals", "CalculationBasis", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goals", "CalculationBasis", c => c.String());
        }
    }
}
