namespace BodyCompositionCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserProgressGraphDataViewWithCheckInId : DbMigration
    {
        public override void Up()
        {
            Sql(@"alter VIEW UserProgressGraphData
                AS 
                select  Date, 
		        Weightinkg, 
		        bodyfat, 
		        GoalWeight, 
		        Goalbodyfat,
		        Userprofileid,
                id CheckInId
		        
		        from UserProgressLogWithGoals
		        
		        union
			        select Date, Weightinkg, bodyfat, null, null, Userprofileid, id 
			        from userprogresslogs		
		        union 
			        select startDate, null, null, StartWeightinkg, null, Userprofileid, null
			        from goals		
		        union 
			        select startDate, null, null, null, Startbodyfat, Userprofileid, null
			        from goals
		        union
			        select enddate, null, null, targetweightinkg, null, Userprofileid, null
			        from goals
		        union 
			        select enddate, null, null, null, targetbodyfat, Userprofileid, null
			        from goals");
		}
        
        public override void Down()
        {

            Sql(@"alter  VIEW dbo.UserProgressGraphData
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
		
	}
    
}
