using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BodyCompositionCalculator.Controllers.API;
using BodyCompositionCalculator.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BodyCompositionCalculator.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        IdentityDbContext _identityContext;
        
        public HomeController()
        {
            _context = new ApplicationDbContext();
            _identityContext = new IdentityDbContext();
        }
        public ActionResult Index()
        {


            //IdentityUser identityUser = _identityContext.Users.SingleOrDefault(u => u.Id == User.Identity.GetUserId());
            //UserProfile userProfile = _context.UserProfiles.SingleOrDefault(u=>u.Id = User.Identity.Name)

            //return View(userProfile);
            return View();
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