namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivityLevelToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
         
            Sql(@"insert into activitylevels (name, value) values('Sedentary',1.2)");
            Sql(@"insert into activitylevels (name, value) values('LightlyActive',1.375)");
            Sql(@"insert into activitylevels (name, value) values('ModeratelyActive',1.55)");
            Sql(@"insert into activitylevels (name, value) values('VeryActive',1.725)");
            Sql(@"insert into activitylevels (name, value) values('ExtraActive',1.9)");

        }
        
        public override void Down()
        {
            DropTable("dbo.ActivityLevels");
        }
    }
}
