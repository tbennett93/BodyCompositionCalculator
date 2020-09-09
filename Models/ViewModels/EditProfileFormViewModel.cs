using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class EditProfileFormViewModel
    {
        public UserProfile UserProfile { get; set; }


        public int HeightInputA { get; set; }
        public int HeightInputB { get; set; }
        public IEnumerable<WeightUnit> WeightUnits { get; set; }
        public IEnumerable<HeightUnit> HeightUnits { get; set; }
        public IEnumerable<Sex> Sexes { get; set; }
        [DisplayName("Activity Level")]
        public IEnumerable<ActivityLevel> ActivityLevels { get; set; }

        public string RedirectionPage { get; set; }


    }
}