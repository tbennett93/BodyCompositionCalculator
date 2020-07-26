using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models
{
    public class Goal
    {
        public int Id { get; set; }
        //TODO - User can't have overlapping goals. If they do, prompt to delete any overlapping ones
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public double StartWeightInKg { get; set; }

        [Required]
        public double TargetWeightInKg { get; set; }
        public double? FinalWeightInKg { get; set; }
        public int? StartBodyFat { get; set; }
        public int? TargetBodyFat { get; set; }
        public int? FinalBodyFat { get; set; }
    }
}