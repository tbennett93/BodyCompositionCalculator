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
        [DisplayName("weight field")]
        [Range(0,10000, ErrorMessage = "Must be above 0")]
        public string WeightInputA { get; set; }
        [Required]
        [DisplayName("lbs")]
        [Range(0,13, ErrorMessage = "Must be between 0 and 13")]
        public string WeightInputB { get; set; }
        public string WeightUnit { get; set; }
        public string PageTitlePrefix { get; set; } = "New";
        public string RedirectionPage { get; set; } = "Home";

        public bool IsBodyFatCalculation { get; set; }
        [RequiredIfBodyFatAsCalcBasis]
        [Range(0,100)]
        [DisplayName("body fat")]
        public int? BodyFat { get; set; }
        public HttpPostedFileBase Photo { get; set; }

    }
}