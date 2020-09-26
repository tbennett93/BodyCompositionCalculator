﻿using System;
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
            var viewModel = (CheckInFormViewModel)validationContext.ObjectInstance; //needs casting to customer as it only returns a type of Object

            if (viewModel.IsBodyFatCalculation)
            {
                if (viewModel.UserProgressLog.BodyFat == null )
                    return new ValidationResult("Body Fat % Required");
            }

            return ValidationResult.Success;
        }

    }
}