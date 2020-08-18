namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLbsAndStoneName : DbMigration
    {
        public override void Up()
        {
            Sql("update weightunits set name = 'st/lbs' where name = 'lbs/st'");
        }
        
        public override void Down()
        {
        }
    }
}
