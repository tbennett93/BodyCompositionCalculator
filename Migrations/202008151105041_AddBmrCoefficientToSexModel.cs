namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBmrCoefficientToSexModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sexes", "BmrAdjustmentValue", c => c.Double(nullable: false));
            Sql("update sexes set BmrAdjustmentValue = 5 where name = 'Male'");
            Sql("update sexes set BmrAdjustmentValue = -161 where name = 'Female'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sexes", "BmrAdjustmentValue");
        }
    }
}
