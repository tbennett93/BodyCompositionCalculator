namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCalculationBasisFieldToGoal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goals", "CalculationBasis", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goals", "CalculationBasis");
        }
    }
}
