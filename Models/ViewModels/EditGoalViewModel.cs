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
        public string StartWeightInputA { get; set; }

        [DisplayName("weight")]
        [Required]
        public string StartWeightInputB { get; set; }

        [DisplayName("weight")]
        [RequiredIfWeightAsCalcBasis]
        public string TargetWeightInputA { get; set; }

        [DisplayName("weight")]
        [RequiredIfWeightAsCalcBasis]
        public string TargetWeightInputB { get; set; }
        public string WeightUnit { get; set; }
        public string Title { get; set; }
        public bool AddAsCheckIn { get; set; } = true;
        public string CalculationBasisChoice { get; set; }
        public SelectList CalculationBasis { get; set; }

        //[BodyFatRequiredIfTracked]
        //public bool TrackBodyFat { get; set; } = true;


    }

}