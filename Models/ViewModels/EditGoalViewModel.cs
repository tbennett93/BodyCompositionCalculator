using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class EditGoalViewModel
    {
        public Goal Goal { get; set; }
        public string StartWeightInputA { get; set; }
        public string StartWeightInputB { get; set; }
        public string TargetWeightInputA { get; set; }
        public string TargetWeightInputB { get; set; }
        public string WeightUnit { get; set; }
        public string Title { get; set; }
    }

}