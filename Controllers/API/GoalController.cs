using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BodyCompositionCalculator.Models;

namespace BodyCompositionCalculator.Controllers.API
{
    public class GoalController : ApiController
    {
        ApplicationDbContext _context;

        public GoalController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: api/Goal
        //TODO delete this 'get all logs' before go live
        [HttpGet]
        [Authorize]
        public Goal GetUserProgressLogs()
        {
            var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            return _context.Goals.SingleOrDefault(u => u.UserProfileId == userId);
        }
    }
}
