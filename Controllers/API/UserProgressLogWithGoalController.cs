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
    public class UserProgressLogWithGoalController : ApiController
    {

        ApplicationDbContext _context;
        //DbContext db;
        public UserProgressLogWithGoalController()
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
                  +"userprofileid "
                  +"from "
                  +"userprogressgraphdata "
                  +"where 1=1" 
                  + "and userprofileid = " + userId
                )
                .ToList();
            //

      var JSONdata = JsonConvert.SerializeObject(queryThree, Formatting.None);
            return queryThree;
 

        }

    }
}

//TODO use a prepared statement here