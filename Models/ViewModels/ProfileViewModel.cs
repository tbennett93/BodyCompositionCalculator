using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public string HeightToDisplay { get; set; }
        public string Photo { get; set; }
        public HttpPostedFileBase PhotoUpload { get; set; }

    }
}