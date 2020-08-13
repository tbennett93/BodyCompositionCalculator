namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserProfilesTableWithFkToSexTable : DbMigration
    {
        public override void Up()
        {

            Sql("insert into sexes (name) values ('Male')");
            Sql("insert into sexes (name) values ('Female')");

            AddColumn("dbo.UserProfiles", "SexId", c => c.Int(nullable: false));
            Sql("update userprofiles set sexid = 1");
            CreateIndex("dbo.UserProfiles", "SexId");
            AddForeignKey("dbo.UserProfiles", "SexId", "dbo.Sexes", "Id", cascadeDelete: true);
            DropColumn("dbo.UserProfiles", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Sex", c => c.String(nullable: false));
            DropForeignKey("dbo.UserProfiles", "SexId", "dbo.Sexes");
            DropIndex("dbo.UserProfiles", new[] { "SexId" });
            DropColumn("dbo.UserProfiles", "SexId");
        }
    }
}
