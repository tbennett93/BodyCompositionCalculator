using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BodyCompositionCalculator.Models;

namespace BodyCompositionCalculator.Controllers
{
    public class TrackerController : Controller
    {

        ApplicationDbContext _context;


        public TrackerController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: Tracker
        public ActionResult Index()
        {
            var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
            var viewModel = Helper_Classes.UserHelpers.GetUserProfile();
            viewModel.WeightUnit = _context.WeightUnits.SingleOrDefault(m=>m.Name == weightUnit);
            return View("Tracker", viewModel);
        }
    }
}