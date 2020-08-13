namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeightUnitsToAlteredTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"Insert into weightunits (name) values ('cm')");
            Sql(@"Insert into weightunits (name) values ('lbs')");
        }
        
        public override void Down()
        {
        }
    }
}
