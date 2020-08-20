using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class CheckInFormViewModel
    {
        public UserProgressLog UserProgressLog { get; set; }
        public double WeightInputA { get; set; }
        public double WeightInputB { get; set; }
        public string WeightUnit { get; set; }
        public string PageTitlePrefix { get; set; } = "New";
    }
}