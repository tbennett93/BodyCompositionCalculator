namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateViewUserProgressGraphData : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE  VIEW dbo.UserProgressGraphData
                AS 
                select  Date, 
		Weightinkg, 
		bodyfat, 
		GoalWeight, 
		Goalbodyfat,
		Userprofileid
		
		from UserProgressLogWithGoals
		
		union
			select Date, Weightinkg, bodyfat, null, null, Userprofileid
			from userprogresslogs		
		union 
			select startDate, null, null, StartWeightinkg, null, Userprofileid
			from goals		
		union 
			select startDate, null, null, null, Startbodyfat, Userprofileid
			from goals
		union
			select enddate, null, null, targetweightinkg, null, Userprofileid
			from goals
		union 
			select enddate, null, null, null, targetbodyfat, Userprofileid
			from goals");
        }
        
        public override void Down()
        {

			Sql("drop view  dbo.UserProgressGraphData");
        }
    }
}
