using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using BodyCompositionCalculator.Models;
using BodyCompositionCalculator.Models.Calculation_Constants;
using Newtonsoft.Json;

namespace BodyCompositionCalculator.Controllers.API
{
    public class UserProgressAllController : ApiController
    {

        ApplicationDbContext _context;
        //DbContext db;
        public UserProgressAllController()
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
                  + "round(weightinkg,1) weightinkg, "
                  + "bodyfat, "
                  + "round(goalweight,1) goalweight, "
                  + "goalbodyfat, "
                  + "userprofileid, "
                  + "'kg' weightunit "
                  + "from "
                  + "userprogressgraphdata "
                  + "where 1=1"
                  + "and userprofileid = " + userId
                )
                .ToList();
            if (weightUnit == WeightUnits.LbsAndStone || weightUnit == WeightUnits.Lbs)
                graphData = _context.Database.SqlQuery<UserProgressGraphDataModel>
                    ("select "
                       + "date, "
                       + "round(weightinkg * 2.20462,1) weightinkg, "
                       + "bodyfat, "
                       + "round(goalweight * 2.20462,1) goalweight, "
                       + "goalbodyfat, "
                       + "userprofileid, "
                       + "'lbs' weightunit "
                       + "from "
                       + "userprogressgraphdata "
                       + "where 1=1"
                       + "and userprofileid = " + userId
                    )
                    .ToList();


            var JSONdata = JsonConvert.SerializeObject(graphData, Formatting.None);
            return graphData;


        }


        //public List<UserProgressGraphDataModel> GetUserProgressLogWithGoals()
        //{

        //    List<UserProgressGraphDataModel> graphData = null;

        //    graphData = _context.Database.SqlQuery<UserProgressGraphDataModel>
        //    ("select "
        //      + "date, "
        //      + "round(weightinkg,1), "
        //      + "bodyfat, "
        //      + "round(goalweight,1), "
        //      + "goalbodyfat, "
        //      + "userprofileid, "
        //      + "'kg' weightunit "
        //      + "from "
        //      + "userprogressgraphdata "
        //      + "where 1=1"
        //      + "and userprofileid = " + 13
        //    )
        //    .ToList();


        //    var JSONdata = JsonConvert.SerializeObject(graphData, Formatting.None);
        //    return graphData;
        //}




    }
}

//TODO use a prepared statement here?