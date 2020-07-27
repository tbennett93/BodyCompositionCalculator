using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using BodyCompositionCalculator.Controllers.API;
using BodyCompositionCalculator.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BodyCompositionCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IdentityDbContext _identityContext;
        private UserProfile currentUserProfile;
        private string userId;


        public HomeController()
        {
            _context = new ApplicationDbContext();
            
            //_identityContext = new IdentityDbContext();


            //if (String.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.GetUserId()))
            //{
             
            //}


        }
        public ActionResult Index()
        {


            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //userId = User.Identity.GetUserId();
                //userId = Helper_Classes.UserHelpers.GetUserId();
                //currentUserProfile =
                //    _context.UserProfiles.SingleOrDefault(u => u.ApplicationUserId == userId)
                currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();

                //If Goal ID Fk is populated, it may be for an end dated goal
                //Goal Id FK will not remove on end dating a goal, it will only update on adding a new goal
                //TODO - clicking New Goal whilst already mid-goal should prompt the user to end the current goal
                if (!currentUserProfile.GoalId.HasValue)
                {
                    //Return view where no goal has been set and no previous goal has been saved
                    return View("HomeNoGoal");


                }

                if (_context.Goals.SingleOrDefault(g => g.Id == currentUserProfile.GoalId.Value).EndDate == null ||
                    _context.Goals.SingleOrDefault(g => g.Id == currentUserProfile.GoalId.Value).EndDate < DateTime.Today)
                {
                    //Return view with prompt to set a new goal and have last goal summary underneath
                    return View("HomeNoGoal");
                }
                //Goal must be active - return view with current goal summary
                return View("HomeWithGoal");
            }

            return View("HomeNoLogin");



        }

     
        public ActionResult NewGoal()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}