namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnitsToWeightUnitsTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"Insert into weightunits (weightunit) values ('cm')");
            Sql(@"Insert into weightunits (weightunit) values ('lbs')");
        }
        
        public override void Down()
        {
        }
    }
}
