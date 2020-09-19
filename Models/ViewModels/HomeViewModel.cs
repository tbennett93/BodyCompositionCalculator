using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class HomeViewModel
    {
        public string StartWeight { get; set; }
        public string CurrentWeight { get; set; }
        public string GoalWeight { get; set; }
        public string WeightProgressPercentage { get; set; }

        public string StartBodyFat { get; set; }
        public string CurrentBodyFat { get; set; }
        public string GoalBodyFat { get; set; }
        public string BodyFatProgressPercentage { get; set; }


        public string TimeRemaining { get; set; }
        public string Calories { get; set; }
        public string Macros { get; set; }  
        public string GoalType { get; set; }
        public bool TrackBodyFat { get; set; }
        public bool IncludeBodyFatSummary { get; set; }
    }
}