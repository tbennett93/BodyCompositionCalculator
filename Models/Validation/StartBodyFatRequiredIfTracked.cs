using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.ViewModels;

namespace BodyCompositionCalculator.Models.Validation
{
    public class StartBodyFatRequiredIfTracked : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (Goal)validationContext.ObjectInstance; //needs casting to customer as it only returns a type of Object

            if (viewModel.TrackBodyFat)
            {
                if(viewModel.StartBodyFat == null || viewModel.StartBodyFat==0)
                    return new ValidationResult("Starting Body Fat Required");
            }

            return ValidationResult.Success;
        }
    }
}