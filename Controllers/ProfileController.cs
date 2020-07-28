﻿using System;
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
        public ActionResult Create(UserProfile newUserProfile)
        {

            //Return the same page with erro 
            if (!ModelState.IsValid)

            {
                IEnumerable<ModelError> errors;
                errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var VARIABLE in errors)
                {
                    System.Diagnostics.Debug.WriteLine(VARIABLE.ErrorMessage);

                }
                return View("Edit", newUserProfile);
            }

            

            if(Helper_Classes.UserHelpers.GetUserProfile()==null)
                _context.UserProfiles.Add(newUserProfile);
            else
            {
                var userProfleId = Helper_Classes.UserHelpers.GetUserProfile().Id;
                var currentUserProfile = _context.UserProfiles.SingleOrDefault(u=>u.Id == userProfleId) ;

                _context.Entry(_context.UserProfiles.SingleOrDefault(u => u.Id == userProfleId)).CurrentValues.SetValues(newUserProfile);
                _context.SaveChanges();
                currentUserProfile = newUserProfile;

            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: Profile/Edit/5
        //TODO can only be accessed if logged in
        public ActionResult Edit()
        {
            UserProfile viewModel;
            if (Helper_Classes.UserHelpers.GetUserProfile() == null)
                viewModel = new UserProfile
                {
                    ApplicationUserId = User.Identity.GetUserId()
                    //,
                    //ApplicationUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId())
                };
            else
                viewModel = Helper_Classes.UserHelpers.GetUserProfile();


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
