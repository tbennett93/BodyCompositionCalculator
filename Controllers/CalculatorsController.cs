using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BodyCompositionCalculator.Controllers
{
    public class CalculatorsController : Controller
    {
        // GET: Calculators
        public ActionResult Index()
        {
            return View();
        }
    }
}