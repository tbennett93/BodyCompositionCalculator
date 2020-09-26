using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Validation;

namespace BodyCompositionCalculator.Models
{
    public class Goal
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [EndDateGreaterThanStartDate]
        public DateTime EndDate { get; set; }

        [Required]
        public double StartWeightInKg { get; set; }
        [Required]
        public double TargetWeightInKg { get; set; }
        public double? FinalWeightInKg { get; set; }
        public int? StartBodyFat { get; set; }
        public int? TargetBodyFat { get; set; }
        public int? FinalBodyFat { get; set; }
 
        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        [Required] 
        public string CalculationBasis { get; set; } = "Weight";
        public bool TrackBodyFat { get; set; } = true;
    }
}