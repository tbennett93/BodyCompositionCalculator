using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Validation;

namespace BodyCompositionCalculator.Models.ViewModels
{
    public class CheckInFormViewModel
    {
        public UserProgressLog UserProgressLog { get; set; }
        [Required]
        [DisplayName("weight")]
        public string WeightInputA { get; set; }
        [Required]
        [DisplayName("weight")]
        public string WeightInputB { get; set; }
        public string WeightUnit { get; set; }
        public string PageTitlePrefix { get; set; } = "New";
        public string RedirectionPage { get; set; } = "Home";

        public bool IsBodyFatCalculation { get; set; }
        [RequiredIfBodyFatAsCalcBasis]
        public int? BodyFat { get; set; }
        public HttpPostedFileBase Photo { get; set; }

    }
}