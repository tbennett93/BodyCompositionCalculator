namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionsToActivityLevelsNames : DbMigration
    {
        public override void Up()
        {
            Sql("update activitylevels set name = 'Lightly Active' where id = 2");
            Sql("update activitylevels set name = 'Moderately Active' where id = 3");
            Sql("update activitylevels set name = 'Very Active' where id = 4");
            Sql("update activitylevels set name = 'Extra Active' where id = 5");
        }
        
        public override void Down()
        {
        }
    }
}
