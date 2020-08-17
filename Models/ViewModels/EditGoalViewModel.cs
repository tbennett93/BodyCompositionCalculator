using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class EditGoalViewModel
    {
        public Goal Goal { get; set; }
        public double StartWeightInputA { get; set; }
        public double StartWeightInputB { get; set; }
        public double TargetWeightInputA { get; set; }
        public double TargetWeightInputB { get; set; }
        public string WeightUnit { get; set; }
    }

}