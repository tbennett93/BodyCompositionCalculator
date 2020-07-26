namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserProfileDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "Sex", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "Bmr", c => c.Double());
            AlterColumn("dbo.UserProfiles", "WeightUnit", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "HeightUnit", c => c.String(nullable: false));
            DropColumn("dbo.UserProfiles", "CurrentBf");
            DropColumn("dbo.UserProfiles", "TargetBf");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "TargetBf", c => c.Int(nullable: false));
            AddColumn("dbo.UserProfiles", "CurrentBf", c => c.Int(nullable: false));
            AlterColumn("dbo.UserProfiles", "HeightUnit", c => c.String());
            AlterColumn("dbo.UserProfiles", "WeightUnit", c => c.String());
            AlterColumn("dbo.UserProfiles", "Bmr", c => c.Double(nullable: false));
            AlterColumn("dbo.UserProfiles", "Sex", c => c.String());
            AlterColumn("dbo.UserProfiles", "LastName", c => c.String());
            AlterColumn("dbo.UserProfiles", "FirstName", c => c.String());
        }
    }
}
