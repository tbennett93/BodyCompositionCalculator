using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BodyCompositionCalculator.Models;
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
            return View();
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: Profile/Create
        //TODO can only be accessed if logged in
        [HttpPost]
        public ActionResult Create(UserProfile userProfile)
        {

            //Return the same page with error reasons. If these don't help, run in debug and check output
            if (!ModelState.IsValid)

            {
                IEnumerable<ModelError> errors;
                errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var VARIABLE in errors)
                {
                    System.Diagnostics.Debug.WriteLine(VARIABLE.ErrorMessage);

                }
                return View("Edit", userProfile);
            }

            
            _context.UserProfiles.Add(userProfile);
            _context.SaveChanges();
            return RedirectToAction("Index", new { controller = "Home" });
        }




        // GET: Profile/Edit/5
        //TODO can only be accessed if logged in
        public ActionResult Edit()
        {
            UserProfile viewModel = new UserProfile
            {
                ApplicationUserId = User.Identity.GetUserId()
            };
            return View(viewModel);
        }

        // POST: Profile/Edit/5
        [HttpPost]  
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
