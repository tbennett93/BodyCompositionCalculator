namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeightUnitNavigationPropertyToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "WeightUnitId", c => c.Int(nullable: false));
            Sql("Update userprofiles set WeightUnitId = 1");
            CreateIndex("dbo.UserProfiles", "WeightUnitId");
            AddForeignKey("dbo.UserProfiles", "WeightUnitId", "dbo.WeightUnits", "Id", cascadeDelete: true);
            DropColumn("dbo.UserProfiles", "WeightUnit");

        }

        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "WeightUnit", c => c.String(nullable: false));
            DropForeignKey("dbo.UserProfiles", "WeightUnitId", "dbo.WeightUnits");
            DropIndex("dbo.UserProfiles", new[] { "WeightUnitId" });
            DropColumn("dbo.UserProfiles", "WeightUnitId");
        }
    }
}
