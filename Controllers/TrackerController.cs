using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BodyCompositionCalculator.Models;
using BodyCompositionCalculator.Models.ViewModels;

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

        public ActionResult MyPhoto(int id)
        {
            var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;

            var viewModel = new ViewPhotoViewModel();
            //viewModel.Photo = _context.UserProgressLogs.SingleOrDefault(m => m.Id == id).Photo.Photo;
            var photoBase64 =  Convert.ToBase64String(_context.UserPhotos.SingleOrDefault(m => m.Id == id).Photo);


            //viewModel.Photo = new HtmlString("<img src=\"data:image/*;base64," + photoBase64 + "\"");
            viewModel.Photo = photoBase64;
            //viewModel.PhotoString = new HtmlString("\"<img src=\"data:image/*;base64," + photoBase64 + "\">");


            return View(viewModel);
        }
    }
}