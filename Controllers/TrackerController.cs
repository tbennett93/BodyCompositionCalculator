using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BodyCompositionCalculator.Controllers
{
    public class TrackerController : Controller
    {
        // GET: Tracker
        public ActionResult Index()
        {
            var viewModel = Helper_Classes.UserHelpers.GetUserProfile();
            return View("Tracker", viewModel);
        }
    }
}