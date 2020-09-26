using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Calculation_Constants;
using BodyCompositionCalculator.Models.ViewModels;

namespace BodyCompositionCalculator.Models.Validation
{
    public class RequiredIfWeightAsCalcBasis : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (EditGoalViewModel)validationContext.ObjectInstance; 

            if (viewModel.CalculationBasisChoice.Equals(CalculationBasis.Weight))
            {
                if (viewModel.TargetWeightInputA == null || viewModel.TargetWeightInputB == null)
                    return new ValidationResult("Target Weight Required");
            }

            return ValidationResult.Success;
        }
    }
}