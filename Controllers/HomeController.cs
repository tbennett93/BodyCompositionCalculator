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

            currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();

            //Home page - returns different pages based on whether logged in/profile created/goal set
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                if (Helper_Classes.UserHelpers.GetUserProfile() == null)
                {
                    return View("HomeNoProfile");
                }
  
                

                //No goal exists for current user
                //TODO - clicking New Goal whilst already mid-goal should prompt the user to end the current goal
                if (_context.Goals.SingleOrDefault(g=>g.UserProfileId == currentUserProfile.Id) == null)
                    //Return view where no goal has been set and no previous goal has been saved
                    return View("HomeNoGoal");
                

                //Goal exists for current user, if the ETD is less than today, return No Goal and populate the page with New Goal button and previous summary
                if (_context.Goals.SingleOrDefault(g => g.UserProfileId == currentUserProfile.Id).EndDate < DateTime.Today)
                {
                    return View("HomeEndedGoal");
                }

                //Goal must be active - return view with current goal summary
                return View("HomeWithGoal");
            }

            return View("HomeNoLogin");



        }

     
        public ActionResult AddNewGoal(Goal newGoal)
        {

            if (!ModelState.IsValid)
                return View("NewGoalForm", newGoal);

            currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;

            //TODO check if goal already exists, if so prompt to delete, if not or expired, overwrite.
            //TODO latest checking always updates final weight

            //No Goal Id on user profile. Add it
            if (_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId) == null)
            {

                newGoal.UserProfileId = userProfileId;
                _context.Goals.Add(newGoal);
                _context.SaveChanges();
                return RedirectToAction("Index", new { controller = "Home" });
            }


            //Update the existing Goal record
            _context.Entry(_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId))
                .CurrentValues
                .SetValues(newGoal);
            _context.SaveChanges();
            return RedirectToAction("Index", new { controller = "Home" });
        }



        public ActionResult NewGoalForm()
        {
            //If no goal found, fetch blank goal page. If existing goal found, fetch existing info into page
            Goal viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            if (_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId) == null)
                viewModel = new Goal();
            
            viewModel = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId);
            //TODO check if goal already exists, if so prompt to delete, if not or expired, overwrite.
            return View(viewModel);
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

        public ActionResult NewCheckInForm()
        {

            //If no log found for that date, fetch blank log page. If existing log found, fetch existing info into page
            UserProgressLog viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            if (_context.UserProgressLogs.SingleOrDefault(g =>
                g.UserProfileId == userProfileId && g.Date == DateTime.Today) == null)
                viewModel = new UserProgressLog();
            else
            { 
                viewModel = _context.UserProgressLogs.SingleOrDefault(g =>
                g.UserProfileId == userProfileId && g.Date == DateTime.Today);
            }

            //TODO check if goal already exists, if so prompt to delete, if not or expired, overwrite.
            return View(viewModel);


        }

        public ActionResult AddNewProgressLog(UserProgressLog userProgressLog)
        {
            if (!ModelState.IsValid)
                return View("NewCheckInForm", userProgressLog);

            currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();
            var userProfileId = currentUserProfile.Id;

            //Check for log on the same date, if it doesn't exist, insert, else update
            if (_context.UserProgressLogs.SingleOrDefault(g =>
                g.UserProfileId == userProfileId && g.Date == userProgressLog.Date) == null)
            {
                //Insert
                userProgressLog.UserProfileId = userProfileId;
                _context.UserProgressLogs.Add(userProgressLog);
                _context.SaveChanges();
                return RedirectToAction("Index", new { controller = "Home" });
            }
            //Update
            _context.Entry(_context.UserProgressLogs.SingleOrDefault(g => g.UserProfileId == userProfileId && g.Date == userProgressLog.Date))
                .CurrentValues
                .SetValues(userProgressLog);

            _context.SaveChanges();
            return RedirectToAction("Index", new { controller = "Home" });

        }
    }
}