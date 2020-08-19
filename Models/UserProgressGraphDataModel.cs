using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models
{
    public class UserProgressGraphDataModel
    {
        public DateTime? Date { get; set; }

        public double? WeightInKg { get; set; }

        public int? BodyFat { get; set; }

        public double? GoalWeight { get; set; }

        public int? GoalBodyFat { get; set; }

        public int userprofileid { get; set; }

        public string WeightUnit { get; set; }


    }
}