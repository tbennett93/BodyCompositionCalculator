namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBmrFromUserProfile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfiles", "Bmr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Bmr", c => c.Double());
        }
    }
}
