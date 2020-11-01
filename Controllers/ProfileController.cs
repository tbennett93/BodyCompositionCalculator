using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.WebPages;
using BodyCompositionCalculator.Helper_Classes.Extension_Classes;
using BodyCompositionCalculator.Models;
using BodyCompositionCalculator.Models.Calculation_Constants;
using BodyCompositionCalculator.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BodyCompositionCalculator.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;
        IdentityDbContext _identityContext;
        public ProfileController()
        {
            _context = new ApplicationDbContext();
            _identityContext = new IdentityDbContext();
        }

        // GET: Profile
        public ActionResult Index()
        {
            var userProfile = Helper_Classes.UserHelpers.GetUserProfile();
            if (userProfile == null)
                return RedirectToAction("Edit");




            var userId = userProfile.Id;
            string heightToDisplay = "";
            string heightUnit = _context.UserProfiles.Where(m=>m.Id==userId).Select(m=>m.HeightUnit.Name).SingleOrDefault();
            double heightInCm = _context.UserProfiles.Where(m => m.Id == userId).Select(m => m.HeightInCm)
                .SingleOrDefault();


            if (heightUnit == HeightUnits.Cm)
                heightToDisplay = heightInCm.ToString() + "cm";
            else if (heightUnit == HeightUnits.Feetandinches)
                heightToDisplay = Calculators.CmToFt(heightInCm).ToString() + "ft" + Calculators.CmToInches(heightInCm).ToString() + "in";

            ProfileViewModel viewModel = new ProfileViewModel
            {
                
                HeightToDisplay = heightToDisplay,
                UserProfile = _context.UserProfiles.
                    Include(m => m.Sex).
                    Include(m => m.HeightUnit).
                    Include(m => m.WeightUnit).
                    Include(m => m.ActivityLevel).
                    ToList().
                    SingleOrDefault(m => m.Id == userId),

            };

            if(_context.UserPhotos.SingleOrDefault(m => m.Id == userProfile.UserPhotoId)?.Photo != null)
                viewModel.Photo =
                    Convert.ToBase64String(_context.UserPhotos.SingleOrDefault(m => m.Id == userProfile.UserPhotoId)?.Photo);


            return View(viewModel);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: Profile/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(EditProfileFormViewModel newUserProfile)
        {
            var heightUnitId = newUserProfile.UserProfile.HeightUnitId;
            var heightUnit = _context.HeightUnits
                .Where(m => m.Id == heightUnitId)
                .Select(m=>m.Name)
                .SingleOrDefault();
            //Return the same page with erro 
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> errors;
                errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var VARIABLE in errors)
                    System.Diagnostics.Debug.WriteLine(VARIABLE.ErrorMessage);

                newUserProfile.WeightUnits = _context.WeightUnits.ToList();
                newUserProfile.HeightUnits = _context.HeightUnits.ToList();
                newUserProfile.Sexes = _context.Sexes.ToList();
                newUserProfile.ActivityLevels = _context.ActivityLevels.ToList();
                newUserProfile.HeightInputA = Convert.ToInt32(GetHeightInCm(1));

                if (heightUnit == HeightUnits.Feetandinches)
                    newUserProfile.HeightInputB = Convert.ToInt32(GetHeightInCm(2));

                return View("Edit", newUserProfile);
            }


            double heightInCm = 0.0d;
            if (heightUnit == HeightUnits.Cm)
                heightInCm = newUserProfile.HeightInputA;
            else if (heightUnit == HeightUnits.Feetandinches)
                heightInCm = Calculators.FeetAndInchesToCM(newUserProfile.HeightInputA, newUserProfile.HeightInputB);

            newUserProfile.UserProfile.HeightInCm = heightInCm;

            if (Helper_Classes.UserHelpers.GetUserProfile()==null)
                _context.UserProfiles.Add(newUserProfile.UserProfile);
                
            else
            {
                var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
                var currentUserProfile = _context.UserProfiles.SingleOrDefault(u=>u.Id == userProfileId) ;
               

                _context.Entry(_context.UserProfiles.SingleOrDefault(u => u.Id == userProfileId)).CurrentValues.SetValues(newUserProfile.UserProfile);

            }
            _context.SaveChanges();
            return RedirectToAction("Index", newUserProfile.RedirectionPage);
        }




        // GET: Profile/Edit/
        [Authorize]
        public ActionResult Edit()
        {
            EditProfileFormViewModel viewModel;
            
            var userProfile = Helper_Classes.UserHelpers.GetUserProfile();

            var returnPage = userProfile == null ? "Home" : "Profile";
            //var returnPage =  "Home";

            if (userProfile == null)
                viewModel = new EditProfileFormViewModel()
                {
                    UserProfile = new UserProfile
                    {
                        ApplicationUserId = User.Identity.GetUserId()
                    },
                    WeightUnits = _context.WeightUnits.ToList(),
                    HeightUnits = _context.HeightUnits.ToList(),
                    Sexes = _context.Sexes.ToList(),
                    ActivityLevels = _context.ActivityLevels.ToList(),
                    RedirectionPage = returnPage
                };
            else
            {
                var valForHeighInputA = Convert.ToInt32(GetHeightInCm(1));
                var valForHeighInputB = Convert.ToInt32(GetHeightInCm(2));
                viewModel = new EditProfileFormViewModel()
                {
                    UserProfile = userProfile,
                    WeightUnits = _context.WeightUnits.ToList(),
                    HeightUnits = _context.HeightUnits.ToList(),
                    Sexes = _context.Sexes.ToList(),
                    ActivityLevels = _context.ActivityLevels.ToList(),
                    HeightInputA = valForHeighInputA,
                    HeightInputB = valForHeighInputB,
                    RedirectionPage = returnPage
                };
            }

            return View(viewModel);
        }

       

        private double GetHeightInCm(int field)
        {
            var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            var heightInCm = _context.UserProfiles
                    .SingleOrDefault(m => m.Id == userProfileId)
                    .HeightInCm
                ;
            var heightUnit = Helper_Classes.UserHelpers.GetHeightUnit();
            double val = 0;

            if (heightUnit == HeightUnits.Cm && field == 1)
                val = heightInCm;
            else if (heightUnit == HeightUnits.Feetandinches)
            {
                if(field == 1 )
                    val = Calculators.CmToFt(heightInCm);
                else if(field == 2)
                    val = Calculators.CmToInches(heightInCm);
            }

            return val;
        }

        public ActionResult EditPhoto(ProfileViewModel profileViewModel)
        {
            if(profileViewModel.PhotoUpload != null & profileViewModel.PhotoUpload.IsImage())
            {
                PhotoManager photoManager = new PhotoManager();
                photoManager.UploadProfilePhotoToDb(profileViewModel.PhotoUpload);
                return RedirectToAction("Index");
            }
            return HttpNotFound();

        }
    }
}
