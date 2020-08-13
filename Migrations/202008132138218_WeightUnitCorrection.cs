namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeightUnitCorrection : DbMigration
    {
        public override void Up()
        {
            Sql("update weightunits set name = 'kg' where name = 'cm'");
        }
        
        public override void Down()
        {
        }
    }
}
