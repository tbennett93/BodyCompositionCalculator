using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models.Validation
{
    public class EndDateGreaterThanStartDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var goal = (Goal)validationContext.ObjectInstance; //needs casting to customer as it only returns a type of Object

            return (goal.StartDate < goal.EndDate)
                ? ValidationResult.Success
                : new ValidationResult("End date must be earlier than the start date");

        }
    }
}