namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHeightUnitsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HeightUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("insert into heightunits (name) values ('cm')");
            Sql("insert into heightunits (name) values ('ft/in')");
            
            AddColumn("dbo.UserProfiles", "HeightUnitId", c => c.Int(nullable: false));
            Sql("update userprofiles set heightunitid = 1");
            CreateIndex("dbo.UserProfiles", "HeightUnitId");
            AddForeignKey("dbo.UserProfiles", "HeightUnitId", "dbo.HeightUnits", "Id", cascadeDelete: true);
            DropColumn("dbo.UserProfiles", "HeightUnit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "HeightUnit", c => c.String(nullable: false));
            DropForeignKey("dbo.UserProfiles", "HeightUnitId", "dbo.HeightUnits");
            DropIndex("dbo.UserProfiles", new[] { "HeightUnitId" });
            DropColumn("dbo.UserProfiles", "HeightUnitId");
            DropTable("dbo.HeightUnits");
        }
    }
}
