using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BodyCompositionCalculator.Models;
using BodyCompositionCalculator.Models.Calculation_Constants;
using Microsoft.Ajax.Utilities;

namespace BodyCompositionCalculator.Controllers.API
{
    public class UserProgressLogController : ApiController
    {
        ApplicationDbContext _context;


        public UserProgressLogController()
        {
            _context = new ApplicationDbContext();

        }
       // GET: api/UserProfileLog
       [HttpGet]
       [Authorize]
        public IEnumerable<UserProgressLog> GetUserProgressLogs()
        {

            var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            return _context.UserProgressLogs
                .Where(u => u.UserProfileId == userId)
                .OrderBy(u => u.Date)
                .ToList();

        }
    //public IEnumerable<UserProgressLogWithGoal> GetUserProgressLogs()
    //   {  

    //       var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;

    //       var query = from UserProgressLog in _context.UserProgressLogs
    //           join Goal in _context.Goals on UserProgressLog.UserProfileId equals Goal.UserProfileId
    //           orderby UserProgressLog.Date
    //           where Goal.UserProfileId == userId
    //           select new
    //           {
    //               StartDate = Goal.StartDate,
    //               EndDate = Goal.EndDate,
    //               StartWeightInKg = Goal.StartWeightInKg,
    //               TargetWeightInKg = Goal.TargetWeightInKg,
    //               StartBodyFat = Goal.StartBodyFat,
    //               TargetBodyFat = Goal.TargetBodyFat
    //           };

    //       return query.ToList();

    //   }

    // GET: api/UserProfileLog/5
    [HttpGet]
        public IHttpActionResult GetUserProgressLogs(int id)
        {
            var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            double weightUnitMultiplier = 1.0;

            if (weightUnit == WeightUnits.Lbs || weightUnit == WeightUnits.LbsAndStone)
                weightUnitMultiplier = 2.20462;


            if (id == Helper_Classes.UserHelpers.GetUserProfile().Id)
                return Ok((from userProgressLog in _context.UserProgressLogs
                        orderby userProgressLog.Date
                        where userProgressLog.UserProfileId == id
                        select new
                        {
                            ProgressLogId = userProgressLog.Id,
                            userProgressLog.Date,
                            userProgressLog.BodyFat,
                            WeightInKg = Math.Round((double) (userProgressLog.WeightInKg * weightUnitMultiplier))
                        }
                    ).ToList());
            //if (id == Helper_Classes.UserHelpers.GetUserProfile().Id)
            //    return
                    //Ok(_context.UserProgressLogs
                    //.Where(u => u.UserProfileId == id)
                    //.OrderBy(u=>u.Date)
                    //.Select(m=>new { m.BodyFat, m.Date, m.WeightInKg})
                    //.ToList());

                    //Ok();

            return Unauthorized();
        }

        // POST: api/UserProfileLog
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserProfileLog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserProfileLog/5
        public void Delete(int id)
        {
        }
    }
}
