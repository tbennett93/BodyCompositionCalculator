using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Validation;

namespace BodyCompositionCalculator.Models
{
    public class UserProgressLogWithGoal
    {

        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public double? WeightInKg { get; set; }

        public int? BodyFat { get; set; }
        public double? GoalWeight { get; set; }

        public int? GoalBodyFat { get; set; }

        public byte?[] Photo { get; set; }

        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        //StartDate = Goal.StartDate,
        //EndDate = Goal.EndDate,
        //StartWeightInKg = Goal.StartWeightInKg,
        //TargetWeightInKg = Goal.TargetWeightInKg,
        //StartBodyFat = Goal.StartBodyFat,
        //TargetBodyFat = Goal.TargetBodyFat
    }
}