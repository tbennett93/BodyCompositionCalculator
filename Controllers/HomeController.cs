using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Net;
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

            HomeViewModel viewModel = new HomeViewModel();


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


                var userProfile = Helper_Classes.UserHelpers.GetUserProfile();

                var userProfileId = userProfile.Id;

                var currentGoal = Helper_Classes.UserHelpers.GetCurrentGoal();

                var trackBodyFat = currentGoal.TrackBodyFat;
                    
                var startWeight = currentGoal.StartWeightInKg;

                var today = DateTime.Today;
                var maxLogDate = Helper_Classes.UserHelpers.GetMaxUserLogDate();
                //var currentWeightDate = _context.UserProgressLogs
                //    .Where(m => m.UserProfileId == userProfileId && m.Date <= today).Select(m => m.Date).OrderByDescending(m => m.Date).First();

                var currentWeight = Helper_Classes.UserHelpers.GetCurrentWeight();

                var currentBodyFat = Helper_Classes.UserHelpers.GetCurrentBodyFat();

                var goalWeight = _context.Goals.SingleOrDefault(m => m.UserProfileId == userProfileId).TargetWeightInKg;
                var goalBodyFat = _context.Goals.SingleOrDefault(m => m.UserProfileId == userProfileId).TargetBodyFat;


                var includeBodyFatSummary = currentGoal.CalculationBasis == CalculationBasis.BodyFat || currentGoal.TrackBodyFat ? true : false;
                viewModel.StartBodyFat = currentGoal.StartBodyFat.ToString();
                viewModel.CurrentBodyFat = currentBodyFat.ToString();
                viewModel.GoalBodyFat = currentGoal.TargetBodyFat.ToString();
                viewModel.IncludeBodyFatSummary = includeBodyFatSummary;
                viewModel.TrackBodyFat = trackBodyFat;
                viewModel.StartWeight = GetWeightString(startWeight);
                viewModel.CurrentWeight = GetWeightString((double) currentWeight);
                viewModel.GoalWeight = GetWeightString(goalWeight);

                //viewModel.CurrentWeight = Convert.ToInt32(currentWeight).ToString();
                //viewModel.GoalWeight = Convert.ToInt32(goalWeight).ToString();


                //Unable to divide by zero so if no difference between start and end weights, divide by 1 instead
                var fullGoalDifference = startWeight - goalWeight == 0 ? 1: startWeight - goalWeight;
        

                viewModel.WeightProgressPercentage = Convert.ToInt32((startWeight-currentWeight)/(fullGoalDifference) * 100).ToString();
                viewModel.TimeRemaining = (currentGoal.EndDate - maxLogDate).TotalDays + " days";
                //var sex = userProfile.Sex.BmrAdjustmentValue;
                var sex = _context.UserProfiles.Include(m=>m.Sex).SingleOrDefault(m=>m.Id == userProfileId).Sex.BmrAdjustmentValue;
                var height = userProfile.HeightInCm;
                var dob = userProfile.DateOfBirth;
                var activityVal = _context.UserProfiles.Include(m => m.ActivityLevel)
                    .SingleOrDefault(m => m.Id == userProfileId).ActivityLevel.Value;

                // Calculate the age.
                var age = today.Year - dob.Year;
                // Go back to the year the person was born in case of a leap year
                if (dob.Date > today.AddYears(-age)) age--;

                var bmr = Calculators.CalculateBmr(sex, Convert.ToInt32(height), age, Convert.ToInt32(currentWeight));


                int dailyCalories = 0;
                if (currentGoal.CalculationBasis.Equals(CalculationBasis.Weight))
                    dailyCalories = Calculators.CalculateDailyCaloriesFromWeight(bmr, activityVal,
                        (double) currentWeight, goalWeight, currentGoal.StartDate, currentGoal.EndDate);
                else if (currentGoal.CalculationBasis.Equals(CalculationBasis.BodyFat))
                    dailyCalories = Calculators.CalculateDailyCaloriesFromBodyFat(bmr, activityVal, (double) currentWeight, (int) currentBodyFat, (int) goalBodyFat, currentGoal.StartDate, currentGoal.EndDate);

                if (dailyCalories > 900)
                    viewModel.Calories = dailyCalories.ToString();
                else
                    viewModel.Calories = "Daily calories too low. Revise Goal.";


                viewModel.Macros = "Not Set";

                viewModel.GoalType = "Edit Goal";
                if (currentGoal.EndDate < DateTime.Today)
                    viewModel.GoalType = "New Goal";

                //Goal must be active - return view with current goal summary
                return View("HomeWithGoal", viewModel);
            }

            return View("HomeNoLogin");



        }

        private static string GetWeightString(double val)
        {
            switch (Helper_Classes.UserHelpers.GetWeightUnit())
            {
                case WeightUnits.Kg:
                    return Math.Round(val, 1) + "kg";
                case WeightUnits.Lbs:
                    return Convert.ToInt32(Calculators.KgsToLbs(val)) + "lbs";
                case WeightUnits.LbsAndStone:
                    return Convert.ToInt32(Calculators.KgsToStone(val)) + "st" + Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(val)) + "lb";
            }

            return "Weight Unit Not Found";
        }


        public ActionResult AddNewGoal(EditGoalViewModel newGoal)
        {

            currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            var dbGoal = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId);
            newGoal.CalculationBasis = new SelectList(new List<string> { "Weight", "Body Fat" }, dbGoal.CalculationBasis);
            newGoal.Title = "Edit Goal";


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

 

            newGoal.Goal.StartBodyFat = newGoal.StartBodyFat;
            newGoal.Goal.TargetBodyFat = newGoal.TargetBodyFat;
            newGoal.Goal.TrackBodyFat = newGoal.TrackBodyFat;

            var trackBf = newGoal.Goal.TrackBodyFat;

            var calculationBasis = newGoal.CalculationBasisChoice;


            double startWeight = 0.0;
            double targetWeight = 0.0;

            if (newGoal.AddAsCheckIn && newGoal.Goal.StartDate <= DateTime.Today)
            {
                var checkInModel = new CheckInFormViewModel
                {
                    WeightUnit = newGoal.WeightUnit,
                    WeightInputA = newGoal.StartWeightInputA,
                    WeightInputB = newGoal.StartWeightInputB,
                    UserProgressLog = new UserProgressLog
                    {
                        Date = newGoal.Goal.StartDate,
                        BodyFat = newGoal.Goal.StartBodyFat
                    }
                };
                UpdateDbWithNewCheckIn(checkInModel);
            }







            if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Kg))
            {
                startWeight = Convert.ToDouble(newGoal.StartWeightInputA);
                targetWeight = Convert.ToDouble(newGoal.TargetWeightInputA);
            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Lbs))
            {
                startWeight = Calculators.LbsToKG(Convert.ToDouble(newGoal.StartWeightInputA));
                targetWeight = Calculators.LbsToKG(Convert.ToDouble(newGoal.TargetWeightInputA));
            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.LbsAndStone))
            {
                startWeight = Calculators.StToKg(Convert.ToDouble(newGoal.StartWeightInputA)) +
                              Calculators.LbsToKG(Convert.ToDouble(newGoal.StartWeightInputB));

                targetWeight = Calculators.StToKg(Convert.ToDouble(newGoal.TargetWeightInputA)) +
                               Calculators.LbsToKG(Convert.ToDouble(newGoal.TargetWeightInputB));
            }


            newGoal.Goal.StartWeightInKg = startWeight;
            newGoal.Goal.TargetWeightInKg = targetWeight;

            //TODO latest checkin always updates final weight on goal
            
            newGoal.Goal.CalculationBasis = (string) calculationBasis;

            //No Goal Id on user profile. Add it
            if (_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId) == null)
            {
                newGoal.Goal.UserProfileId = userProfileId;
                _context.Goals.Add(newGoal.Goal);
            }
            else
            {
                //Update the existing Goal record
                _context.Entry(_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId))
                    .CurrentValues
                    .SetValues(newGoal.Goal);
            }
            _context.SaveChanges();


            //Overwrite new target weight with estimated one if using body fat to calculate goal - ordering is important here as the db needs to have been updated already
            if (calculationBasis.Equals(CalculationBasis.BodyFat))
            {
                targetWeight = Calculators.CalculateEstimatedGoalWeight((double)Helper_Classes.UserHelpers.GetCurrentWeight(),
                    (int)Helper_Classes.UserHelpers.GetCurrentBodyFat(), (int)newGoal.Goal.TargetBodyFat);

                newGoal.Goal.TargetWeightInKg = targetWeight;

                _context.Entry(_context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId))
                    .CurrentValues
                    .SetValues(newGoal.Goal);

                _context.SaveChanges();
            }



            return RedirectToAction("Index", new { controller = "Home" });

        }



        public ActionResult NewGoalForm()
        {
            //If no goal found, fetch blank goal page. If existing goal found, fetch existing info into page
            EditGoalViewModel viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            string startWeightInputA = "";
            string startWeightInputB = "";
            string targetWeightInputA = "";
            string targetWeightInputB = "";

            string weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            var dbGoal = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId);
            


            //No existing goal
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
                    WeightUnit = weightUnit,
                    Title = "New Goal",
                    CalculationBasis = new SelectList(new List<string>{"Weight", "Body Fat"},dbGoal.CalculationBasis)
                };
            //Existing goal
            else
            {
                double startWeight = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId).StartWeightInKg;
                double targetWeight = _context.Goals.SingleOrDefault(g => g.UserProfileId == userProfileId).TargetWeightInKg;
                if (weightUnit.Equals(WeightUnits.Kg))
                {
                    startWeightInputA = Convert.ToInt32(startWeight).ToString();
                    targetWeightInputA = Convert.ToInt32(targetWeight).ToString();
                }
                if (weightUnit.Equals(WeightUnits.Lbs))
                {
                    startWeightInputA = Convert.ToInt32(Calculators.KgsToLbs(startWeight)).ToString();
                    targetWeightInputA = Convert.ToInt32(Calculators.KgsToLbs(targetWeight)).ToString();
                }
                else if (weightUnit == WeightUnits.LbsAndStone)
                {
                    startWeightInputA = Convert.ToInt32(Calculators.KgsToStone(startWeight)).ToString();
                    startWeightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(startWeight)).ToString();
                    targetWeightInputA = Convert.ToInt32(Calculators.KgsToStone(targetWeight)).ToString();
                    targetWeightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(targetWeight)).ToString();
                }

                viewModel = new EditGoalViewModel
                {
                    Goal = dbGoal,
                    StartWeightInputA = startWeightInputA,
                    StartWeightInputB = startWeightInputB,
                    TargetWeightInputA = targetWeightInputA,
                    TargetWeightInputB = targetWeightInputB,
                    WeightUnit = weightUnit,
                    Title = "Edit Goal",
                    CalculationBasis = new SelectList(new List<string> { "Weight", "Body Fat" }, dbGoal.CalculationBasis)

                };
            }

            viewModel.StartBodyFat = viewModel.Goal.StartBodyFat;
            viewModel.TargetBodyFat = viewModel.Goal.TargetBodyFat;
            viewModel.TrackBodyFat = viewModel.Goal.TrackBodyFat;

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

        //[Route("Home/NewCheckInForm")]

        public ActionResult NewCheckInForm(string pageFrom)
        {
            //Used when creating a new check in

            //If no log found for that date, fetch blank log page. If existing log found, fetch existing info into page
            CheckInFormViewModel viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            string weightInputA = "";
            string weightInputB = "";
            var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            double weight = 0.0;

            if (_context.UserProgressLogs.SingleOrDefault(g =>
                g.UserProfileId == userProfileId && g.Date == DateTime.Today) == null)
                viewModel = new CheckInFormViewModel
                {
                    UserProgressLog = new UserProgressLog()
                };

            else
            {
                viewModel = new CheckInFormViewModel
                {
                    UserProgressLog = _context.UserProgressLogs.SingleOrDefault(g =>
                        g.UserProfileId == userProfileId && g.Date == DateTime.Today),

                };
                weight = (double) viewModel.UserProgressLog.WeightInKg;
            }

            viewModel.BodyFat = viewModel.UserProgressLog.BodyFat;



            if (weightUnit.Equals(WeightUnits.Kg))
                weightInputA = Convert.ToInt32(weight).ToString();
            else if (weightUnit.Equals(WeightUnits.Lbs))
                weightInputA = Convert.ToInt32(Calculators.KgsToLbs(weight)).ToString();
            else if (weightUnit.Equals(WeightUnits.LbsAndStone))
            {
                weightInputA = Convert.ToInt32(Calculators.KgsToStone(weight)).ToString();
                weightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(weight)).ToString();
            }
            viewModel.WeightInputA = weightInputA;
            viewModel.WeightInputB = weightInputB;  
            viewModel.WeightUnit = weightUnit;
            viewModel.RedirectionPage = pageFrom;

            viewModel.IsBodyFatCalculation =
                Helper_Classes.UserHelpers.GetCurrentGoal().CalculationBasis == CalculationBasis.BodyFat ? true : false;

            return View(viewModel);
        }

        [Route("Home/NewCheckInForm/{id:int}")]
        //Used when 'editing' a check in from the tracker
        public ActionResult NewCheckInForm(int id)
        {
            //If no log found for that date, fetch blank log page. If existing log found, fetch existing info into page
            CheckInFormViewModel viewModel;
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            string weightInputA = "";
            string weightInputB = "";
            var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            UserProgressLog userProgressLog = _context.UserProgressLogs.SingleOrDefault(m => m.Id == id);
            double weight = userProgressLog.WeightInKg.Value;

            if (weightUnit.Equals(WeightUnits.Kg))
                weightInputA = Convert.ToInt32(weight).ToString();
            else if (weightUnit.Equals(WeightUnits.Lbs))
            {
                weightInputA = Convert.ToInt32(Calculators.KgsToLbs(weight)).ToString();
            }
            else if (weightUnit.Equals(WeightUnits.LbsAndStone))
            {
                weightInputA = Convert.ToInt32(Calculators.KgsToStone(weight)).ToString();
                weightInputB = Convert.ToInt32(Calculators.KgsToLbsRemainingFromStone(weight)).ToString();
            }

            viewModel = new CheckInFormViewModel
            {
                UserProgressLog = userProgressLog,
                WeightInputA = weightInputA,
                WeightInputB = weightInputB,
                WeightUnit = weightUnit,
                PageTitlePrefix = "Edit",
                RedirectionPage = "Tracker"
            };

            viewModel.BodyFat = viewModel.UserProgressLog.BodyFat;
            viewModel.IsBodyFatCalculation =
                Helper_Classes.UserHelpers.GetCurrentGoal().CalculationBasis == CalculationBasis.BodyFat ? true : false;

            return View("NewCheckInForm",viewModel);
        }


        private void UpdateDbWithNewCheckIn(CheckInFormViewModel formUserProgressLog)
        {


            currentUserProfile = Helper_Classes.UserHelpers.GetUserProfile();
            var userProfileId = currentUserProfile.Id;


            double startWeight = 0.0;
            double targetWeight = 0.0;

            if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Kg))
            {
                startWeight = Convert.ToDouble(formUserProgressLog.WeightInputA);
            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.Lbs))
            {
                startWeight = Calculators.LbsToKG(Convert.ToDouble(formUserProgressLog.WeightInputA));
            }
            else if (Helper_Classes.UserHelpers.GetWeightUnit().Equals(WeightUnits.LbsAndStone))
            {
                startWeight = Calculators.StToKg(Convert.ToDouble(formUserProgressLog.WeightInputA)) +
                              Calculators.LbsToKG(Convert.ToDouble(formUserProgressLog.WeightInputB));
            }

            formUserProgressLog.UserProgressLog.WeightInKg = Convert.ToDouble(startWeight);

            //Check for log on the same date, if it doesn't exist, insert, else update
            if (_context.UserProgressLogs.SingleOrDefault(g =>
                g.UserProfileId == userProfileId && g.Date == formUserProgressLog.UserProgressLog.Date) == null)
            {
                //Insert
                formUserProgressLog.UserProgressLog.UserProfileId = userProfileId;
                _context.UserProgressLogs.Add(formUserProgressLog.UserProgressLog);
            }
            else
            {
                var progressLogId = _context.UserProgressLogs.SingleOrDefault(g =>
                    g.UserProfileId == userProfileId && g.Date == formUserProgressLog.UserProgressLog.Date).Id;
                formUserProgressLog.UserProgressLog.Id = progressLogId;
                formUserProgressLog.UserProgressLog.UserProfileId = userProfileId;

                _context.Entry(_context.UserProgressLogs.
                        SingleOrDefault(g => g.UserProfileId == userProfileId && g.Date == formUserProgressLog.UserProgressLog.Date))
                    .CurrentValues
                    .SetValues(formUserProgressLog.UserProgressLog);
            }
            _context.SaveChanges();
                
            //Update

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

            if (formUserProgressLog.Photo != null)
            {
                //TODO Insert new photo into UserPhotos.Photo and assign UserPhotos.Id to Userprogresslogs.userphoto. Separation of concerns - don't have this method handle the specifics of the logic
                byte[] uploadedFile = new byte[formUserProgressLog.Photo.InputStream.Length];

                formUserProgressLog.Photo.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                var newPhoto = new UserPhoto
                {
                    Photo = uploadedFile    
                };

                _context.UserPhotos.Add(newPhoto);
                _context.SaveChanges();

            }



            formUserProgressLog.UserProgressLog.BodyFat = formUserProgressLog.BodyFat;
            UpdateDbWithNewCheckIn(formUserProgressLog);

            return RedirectToAction("Index", new { controller = formUserProgressLog.RedirectionPage });

        }

        [HttpDelete]
        [Authorize]
        public ActionResult DeleteUserProgressLog(int id)
        {

            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            //var userCheck = _context.UserProfiles.Include(m=>m.UserProgressLog).SingleOrDefault(m=>m.);
            var progressLogInDb = _context.UserProgressLogs.SingleOrDefault(c => c.Id == id && c.UserProfileId == userProfileId);

            if (progressLogInDb == null)    
                throw new HttpException("Not Found");

            _context.UserProgressLogs.Remove(progressLogInDb);
            _context.SaveChanges();
            return RedirectToAction("Index", "Tracker");


        }
       
    }
}

//TODO - A button to switch between 'all historical' and 'relevevant to goal' data 