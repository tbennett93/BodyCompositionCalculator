using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BodyCompositionCalculator.Models;
using Newtonsoft.Json;

namespace BodyCompositionCalculator.Controllers.API
{
    public class UserProgressRelevantController : ApiController
    {

        ApplicationDbContext _context;
        //DbContext db;
        public UserProgressRelevantController()
        {
            _context = new ApplicationDbContext();
            //db = new DbContext();
        }
        // GET: UserProgressLogWithGoal
        [HttpGet]
        //[Authorize]
        public List<UserProgressGraphDataModel> GetUserProgressLogWithGoals()
        {

            var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;

            var queryThree = _context.Database.SqlQuery<UserProgressGraphDataModel>
                ( "select "            
                  +"date, "
                  +"weightinkg, "    
                  +"bodyfat, "
                  +"goalweight, "
                  +"goalbodyfat, "
                  +"goals.userprofileid "
                  +"from "
                  +"userprogressgraphdata, "
                  + "(select startdate, enddate, goals.userprofileid "
                  + "from goals ) goals "
                  + "where 1=1 "
                  + "and userprogressgraphdata.date >= goals.startdate "
                  + "and userprogressgraphdata.date <= goals.enddate "
                  + "and userprogressgraphdata.userprofileid = goals.userprofileid "
                  + "and userprogressgraphdata.userprofileid = " + userId

                )
                .ToList();
         
                 // + "and userprofileid = " + userId
                 //

            //+"and date >= (select startdate "
            //    + "from goals "
            //    + "where goals.userprofileid = userId) "
            //
            //+"and date <= (select enddate "
            //    + "from goals "
            //    + "where goals.userprofileid = userId)"

            var JSONdata = JsonConvert.SerializeObject(queryThree, Formatting.None);
            return queryThree;
 

        }

    }
}

//TODO use a prepared statement here
//TODO DRY Principle with other user progress controller