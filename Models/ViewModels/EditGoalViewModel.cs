using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BodyCompositionCalculator.Models.Validation;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class EditGoalViewModel
    {
        public Goal Goal { get; set; }

        [DisplayName("weight")]
        [Required]
        [Range(0,10000, ErrorMessage = "Must be a whole number above 0")]
        public string StartWeightInputA { get; set; }

        [DisplayName("weight")]
        [Required]
        [Range(0,13, ErrorMessage = "Must be a whole number between 0 and 13")]
        public string StartWeightInputB { get; set; }

        [DisplayName("weight")]
        [RequiredIfWeightAsCalcBasis]
        [Range(0, 10000, ErrorMessage = "Must be a whole number above 0")]
        public string TargetWeightInputA { get; set; }

        [DisplayName("weight")]
        [RequiredIfWeightAsCalcBasis]
        [Range(0, 13, ErrorMessage = "Must be a whole number between 0 and 13")]

        public string TargetWeightInputB { get; set; }

        public string WeightUnit { get; set; }
        public string Title { get; set; }
        public bool AddAsCheckIn { get; set; } = true;
        public string CalculationBasisChoice { get; set; }
        public SelectList CalculationBasis { get; set; }

        [StartBodyFatRequiredIfTracked]
        [Range(0,100, ErrorMessage = "Must be a whole number between 0 and 100")]
        [DisplayName("body fat")]
        public int? StartBodyFat { get; set; }

        [Range(0, 100, ErrorMessage = "Must be a whole number between 0 and 100")]
        [TargetBodyFatRequiredIfTracked]
        [DisplayName("body fat")]

        public int? TargetBodyFat { get; set; }

        public bool TrackBodyFat { get; set; }

        //[BodyFatRequiredIfTracked]
        //public bool TrackBodyFat { get; set; } = true;


    }

}