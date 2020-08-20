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
using BodyCompositionCalculator.Models.Calculation_Constants;
using BodyCompositionCalculator.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BodyCompositionCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IdentityDbContext _identityContext;
        private UserProfile currentUserProfile;


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
                    return View("HomeNoProfile");
                
                
                //No goal exists for current user
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

     
        public ActionResult AddNewGoal(EditGoalViewModel newGoal)
        {
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(error);
                    }
                }
                return View("NewGoalForm", newGoal);
            }

            double startWeight = 0.0;
            double targetWeight = 0.0;

            if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Kg))
            {
                startWeight = newGoal.StartWeightInputA;
                targetWeight = newGoal.TargetWeightInputA;
            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Lbs))
            {
                startWeight = Calculators.LbsToKG(newGoal.StartWeightInputA);
                targetWeight = Calculators.LbsToKG(newGoal.TargetWeightInputA);
            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.LbsAndStone))
            {
                startWeight = Calculators.StToKg(newGoal.StartWeightInputA) +
                              Calculators.LbsToKG(newGoal.StartWeightInputB);

                targetWeight = Calculators.StToKg(newGoal.TargetWeightInputA) +
                               Calculators.LbsToKG(newGoal.TargetWeightInputB);
            }

            newGoal.Goal.StartWeightInKg = startWeight;
            newGoal.Goal.TargetWeightInKg = targetWeight;

            currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;

            //TODO latest checkin always updates final weight on goal

            //No Goal Id on user profile. Add it
            if (_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId) == null)
            {

                newGoal.Goal.UserProfileId = userProfileId;
                _context.Goals.Add(newGoal.Goal);
                _context.SaveChanges();
                return RedirectToAction("Index", new { controller = "Home" });
            }


            
            //Update the existing Goal record
            _context.Entry(_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId))
                .CurrentValues
                .SetValues(newGoal.Goal);
            _context.SaveChanges();
            return RedirectToAction("Index", new { controller = "Home" });
        }



        public ActionResult NewGoalForm()
        {
            //If no goal found, fetch blank goal page. If existing goal found, fetch existing info into page
            EditGoalViewModel viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            int startWeightInputA = 0;
            int startWeightInputB = 0;
            int targetWeightInputA = 0;
            int targetWeightInputB = 0;

            string weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();




            if (_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId) == null)
                viewModel = new EditGoalViewModel()
                {
                    Goal = new Goal
                    {
                        UserProfileId = userProfileId,
                        StartDate = DateTime.Today,
                        EndDate = DateTime.Today.AddDays(30)
                    },
                    StartWeightInputA = startWeightInputA,
                    StartWeightInputB = startWeightInputB,
                    TargetWeightInputA = targetWeightInputA,
                    TargetWeightInputB = targetWeightInputB,
                    WeightUnit = weightUnit
                };
            else
            {
                double startWeight = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId).StartWeightInKg;
                double targetWeight = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId).TargetWeightInKg;
                if (weightUnit.Equals(WeightUnits.Kg))
                {
                    startWeightInputA = Convert.ToInt32(startWeight);
                    targetWeightInputA = Convert.ToInt32(targetWeight);
                }
                if (weightUnit.Equals(WeightUnits.Lbs))
                {
                    startWeightInputA = Convert.ToInt32(Calculators.KgsToLbs(startWeight));
                    targetWeightInputA = Convert.ToInt32(Calculators.KgsToLbs(targetWeight));
                }
                else if (weightUnit == WeightUnits.LbsAndStone)
                {
                    startWeightInputA = Convert.ToInt32(Calculators.KgsToStone(startWeight));
                    startWeightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(startWeight));
                    targetWeightInputA = Convert.ToInt32(Calculators.KgsToStone(targetWeight));
                    targetWeightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(targetWeight));
                }

                viewModel = new EditGoalViewModel
                {
                    Goal = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId),
                    StartWeightInputA = startWeightInputA,
                    StartWeightInputB = startWeightInputB,
                    TargetWeightInputA = targetWeightInputA,
                    TargetWeightInputB = targetWeightInputB,
                    WeightUnit = weightUnit
                };
            }
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

        [Route("Home/NewCheckInForm")]

        public ActionResult NewCheckInForm()
        {
            //If no log found for that date, fetch blank log page. If existing log found, fetch existing info into page
            CheckInFormViewModel viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            double weightInputA = 0.0;
            double weightInputB = 0.0;
            var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            double weight = _context.UserProfiles.Where(m => m.Id == userProfileId).Select(m => m.HeightInCm)
                .SingleOrDefault();

            if (weightUnit.Equals(WeightUnits.Kg))
                weightInputA = Convert.ToInt32(weight);
            else if (weightUnit.Equals(WeightUnits.Lbs))
            {
                weightInputA = Convert.ToInt32(Calculators.KgsToLbs(weight));
            }
            else if (weightUnit.Equals(WeightUnits.LbsAndStone))
            {
                weightInputA = Convert.ToInt32(Calculators.KgsToStone(weight));
                weightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(weight));
            }
            if (_context.UserProgressLogs.SingleOrDefault(g =>
                g.UserProfileId == userProfileId && g.Date == DateTime.Today) == null)
                viewModel = new CheckInFormViewModel
                {
                    UserProgressLog = new UserProgressLog(),
                    WeightInputA = weightInputA,
                    WeightInputB = weightInputB,
                    WeightUnit = weightUnit
                };
            else
            {
                viewModel = new CheckInFormViewModel
                {
                    UserProgressLog = _context.UserProgressLogs.SingleOrDefault(g =>
                        g.UserProfileId == userProfileId && g.Date == DateTime.Today),
                    WeightInputA = weightInputA,
                    WeightInputB = weightInputB,
                    WeightUnit = weightUnit
                };
            }
            return View(viewModel);
        }

        [Route("Home/NewCheckInForm/{id:int}")]

        public ActionResult NewCheckInForm(int id)
        {
            //If no log found for that date, fetch blank log page. If existing log found, fetch existing info into page
            CheckInFormViewModel viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            double weightInputA = 0.0;
            double weightInputB = 0.0;
            var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            double weight = _context.UserProfiles.Where(m => m.Id == userProfileId).Select(m => m.HeightInCm)
                .SingleOrDefault();

            if (weightUnit.Equals(WeightUnits.Kg))
                weightInputA = Convert.ToInt32(weight);
            else if (weightUnit.Equals(WeightUnits.Lbs))
            {
                weightInputA = Convert.ToInt32(Calculators.KgsToLbs(weight));
            }
            else if (weightUnit.Equals(WeightUnits.LbsAndStone))
            {
                weightInputA = Convert.ToInt32(Calculators.KgsToStone(weight));
                weightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(weight));
            }

            viewModel = new CheckInFormViewModel
            {
                UserProgressLog = _context.UserProgressLogs.SingleOrDefault(m => m.Id == id),
                WeightInputA = weightInputA,
                WeightInputB = weightInputB,
                WeightUnit = weightUnit
            };

            return View("NewCheckInForm",viewModel);
        }


        public ActionResult AddNewProgressLog(CheckInFormViewModel formUserProgressLog)
        {

            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(error);
                    }
                }
                return View("NewCheckInForm", formUserProgressLog);

            }

            currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();
            var userProfileId = currentUserProfile.Id;


            double startWeight = 0.0;
            double targetWeight = 0.0;

            if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Kg))
            {
                startWeight = formUserProgressLog.WeightInputA;
            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Lbs))
            {
                startWeight = Calculators.LbsToKG(formUserProgressLog.WeightInputA);

            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.LbsAndStone))
            {
                startWeight = Calculators.StToKg(formUserProgressLog.WeightInputA) +
                              Calculators.LbsToKG(formUserProgressLog.WeightInputB);

            }

            formUserProgressLog.UserProgressLog.WeightInKg = startWeight;

            //Check for log on the same date, if it doesn't exist, insert, else update
            if (_context.UserProgressLogs.SingleOrDefault(g =>
                g.UserProfileId == userProfileId && g.Date == formUserProgressLog.UserProgressLog.Date) == null)
            {
                //Insert
                formUserProgressLog.UserProgressLog.UserProfileId = userProfileId;
                _context.UserProgressLogs.Add(formUserProgressLog.UserProgressLog);
                _context.SaveChanges();
                return RedirectToAction("Index", new { controller = "Home" });
            }
            //Update
            _context.Entry(_context.UserProgressLogs.
                    SingleOrDefault(g => g.UserProfileId == userProfileId && g.Date == formUserProgressLog.UserProgressLog.Date))
                .CurrentValues
                .SetValues(formUserProgressLog.UserProgressLog);

            _context.SaveChanges();
            return RedirectToAction("Index", new { controller = "Home" });

        }
        //public ActionResult NewCheckInForm()
        //{

        //    //If no log found for that date, fetch blank log page. If existing log found, fetch existing info into page
        //    CheckInFormViewModel viewModel = new CheckInFormViewModel();
        //    var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
        //    viewModel.WeightUnit = "kg";
        //    viewModel.WeightInputA = 180.0;
        //    viewModel.WeightInputB = 0.0;
        //    if (_context.UserProgressLogs.SingleOrDefault(g =>
        //        g.UserProfileId == userProfileId && g.Date == DateTime.Today) == null)
        //        viewModel.UserProgressLog = new UserProgressLog();


        //    else
        //    {
        //        viewModel.UserProgressLog = _context.UserProgressLogs.SingleOrDefault(g =>
        //        g.UserProfileId == userProfileId && g.Date == DateTime.Today);
        //    }

        //    return View(viewModel);


        //}

        //public ActionResult AddNewProgressLog(CheckInFormViewModel userProgressLogFromForm)
        //{
        //    if (!ModelState.IsValid)
        //        return View("NewCheckInForm", userProgressLogFromForm);

        //    currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();
        //    var userProfileId = currentUserProfile.Id;

        //    userProgressLogFromForm.UserProgressLog.WeightInKg = 0.0;

        //    //Check for log on the same date, if it doesn't exist, insert, else update
        //    if (_context.UserProgressLogs.SingleOrDefault(g =>
        //        g.UserProfileId == userProfileId && g.Date == userProgressLogFromForm.UserProgressLog.Date) == null)
        //    {
        //        //Insert
        //        userProgressLogFromForm.UserProgressLog.UserProfileId = userProfileId;
        //        _context.UserProgressLogs.Add(userProgressLogFromForm.UserProgressLog);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index", new { controller = "Home" });
        //    }
        //    //Update
        //    _context.Entry(_context.UserProgressLogs.SingleOrDefault(g => g.UserProfileId == userProfileId && g.Date == userProgressLogFromForm.UserProgressLog.Date))
        //        .CurrentValues
        //        .SetValues(userProgressLogFromForm.UserProgressLog);

        //    _context.SaveChanges();
        //    return RedirectToAction("Index", new { controller = "Home" });

        //}
    }
}

//TODO - A button to switch between 'all historical' and 'relevevant to goal' data 