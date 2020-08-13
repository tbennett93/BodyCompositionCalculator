using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class EditProfileFormViewModel
    {
        public UserProfile UserProfile { get; set; }
        public IEnumerable<WeightUnit> WeightUnits { get; set; }



    }
}