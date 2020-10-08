using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BodyCompositionCalculator.Models;
using BodyCompositionCalculator.Models.ViewModels;

namespace BodyCompositionCalculator.Controllers
{
    public class PhotoController : Controller
    {
        // GET: Photo
        public ActionResult Photo(int id)
        {

            if (new ApplicationDbContext().UserPhotos.SingleOrDefault(m => m.Id == id).Photo == null)
                return HttpNotFound();

            var dbPhoto =
                Convert.ToBase64String(new ApplicationDbContext().UserPhotos.SingleOrDefault(m => m.Id == id).Photo);
            //var imgSrc = "'" + String.Format("data:image/jpg;base64,{0}", dbPhoto) + "'";
            var imgSrc = "'" + String.Format("data:image/jpg;base64,{0}", dbPhoto) + "'";
            var img = new HtmlString(String.Format("<img  class='img-thumbnail' src={0}/>", imgSrc));

            var viewModel = new ViewPhotoViewModel();
            viewModel.Photo = dbPhoto;
            return View(viewModel);
        }
    }
}