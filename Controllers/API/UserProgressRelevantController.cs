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
using BodyCompositionCalculator.Models.Calculation_Constants;
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
        [Authorize]
        public List<UserProgressGraphDataModel> GetUserProgressLogWithGoals()
        {

            var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            List<UserProgressGraphDataModel> graphData = null;

            if (weightUnit == WeightUnits.Kg)
                graphData = _context.Database.SqlQuery<UserProgressGraphDataModel>
                    ("select "
                     + "date, "
                     + "round(weightinkg,1) weightinkg,  "
                     + "bodyfat, "
                     + "round(goalweight,1) goalweight, "
                     + "goalbodyfat, "
                     + "goals.userprofileid, "
                     + "'kg' weightunit "
                     + "from "
                     + "userprogressgraphdata, "
                     + "(select startdate, enddate, goals.userprofileid "
                     + "from goals ) goals "
                     + "where 1=1 "
                     + "and userprogressgraphdata.date >= goals.startdate "
                     + "and userprogressgraphdata.date <= goals.enddate "
                     + "and userprogressgraphdata.userprofileid = goals.userprofileid "
                     + "and userprogressgraphdata.userprofileid = " + userId

                    )
                    .ToList();

            if (weightUnit == WeightUnits.LbsAndStone || weightUnit == WeightUnits.Lbs)
                graphData = _context.Database.SqlQuery<UserProgressGraphDataModel>
                ("select "
                  + "date, "
                  + "round(weightinkg* 2.20462,1) weightinkg, "
                  + "bodyfat, "
                  + "round(goalweight* 2.20462,1) goalweight, "
                  + "goalbodyfat, "
                  + "goals.userprofileid, "
                  + "'lbs' weightunit "
                  + "from "
                  + "userprogressgraphdata, "
                  + "(select startdate, enddate, goals.userprofileid "
                  + "from goals ) goals "
                  + "where 1=1 "
                  + "and userprogressgraphdata.date >= goals.startdate "
                  + "and userprogressgraphdata.date <= goals.enddate "
                  + "and userprogressgraphdata.userprofileid = goals.userprofileid "
                  + "and userprogressgraphdata.userprofileid = " + userId

                )
                .ToList();

            var JSONdata = JsonConvert.SerializeObject(graphData, Formatting.None);
            return graphData;
 

        }

    }
}

//TODO use a prepared statement here
//TODO DRY Principle with other user progress controller