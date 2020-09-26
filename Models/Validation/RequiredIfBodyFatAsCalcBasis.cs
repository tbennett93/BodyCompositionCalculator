using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Calculation_Constants;
using BodyCompositionCalculator.Models.ViewModels;

namespace BodyCompositionCalculator.Models.Validation
{
    public class RequiredIfBodyFatAsCalcBasis : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (CheckInFormViewModel)validationContext.ObjectInstance; 

            if (viewModel.IsBodyFatCalculation)
            {
                if (viewModel.BodyFat == null )
                    return new ValidationResult("Body Fat % Required");
            }

            return ValidationResult.Success;
        }

    }
}