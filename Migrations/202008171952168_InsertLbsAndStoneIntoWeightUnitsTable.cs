namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertLbsAndStoneIntoWeightUnitsTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into weightunits (name) values ('lbs/st')");
        }
        
        public override void Down()
        {
        }
    }
}
