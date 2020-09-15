﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Calculation_Constants;
using BodyCompositionCalculator.Models.ViewModels;

namespace BodyCompositionCalculator.Models.Validation
{
    public class TargetBodyFatRequiredIfTracked : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (Goal)validationContext.ObjectInstance; //needs casting to customer as it only returns a type of Object

            if (viewModel.TrackBodyFat || viewModel.CalculationBasis.Equals(CalculationBasis.BodyFat))
            {
                if(viewModel.TargetBodyFat == null || viewModel.TargetBodyFat == 0 )
                    return new ValidationResult("Target Body Fat Required");
            }

            return ValidationResult.Success;
        }
    }
}