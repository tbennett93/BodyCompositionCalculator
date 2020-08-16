namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHeightInCmToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "HeightInCm", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "HeightInCm", c => c.Int(nullable: false));
        }
    }
}
