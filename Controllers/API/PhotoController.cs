using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using BodyCompositionCalculator.Models;

namespace BodyCompositionCalculator.Controllers.API
{
    public class PhotoController : ApiController
    {

        public IHttpActionResult GetPhoto(int id)
        {



            if (new ApplicationDbContext().UserPhotos.SingleOrDefault(m => m.Id == id).Photo == null)
                return NotFound();

            var dbPhoto =
                Convert.ToBase64String(new ApplicationDbContext().UserPhotos.SingleOrDefault(m => m.Id == id).Photo);
            //var imgSrc = "'" + String.Format("data:image/jpg;base64,{0}", dbPhoto) + "'";
            var imgSrc = "'" + String.Format("data:image/jpg;base64,{0}", dbPhoto) + "'";
            var img = new HtmlString(String.Format("<img data-toggle='popover' class='img-thumbnail' src={0}/>", imgSrc));



            //return Ok(img.ToString());
            return Ok("<p>TEST</p>");


        } 
    }
}
