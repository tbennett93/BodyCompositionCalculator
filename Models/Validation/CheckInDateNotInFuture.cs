using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models.Validation
{
    public class CheckInDateNotInFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userProgressLog = (UserProgressLog)validationContext.ObjectInstance; //needs casting to customer as it only returns a type of Object

            return (userProgressLog.Date <= DateTime.Today)
                ? ValidationResult.Success
                : new ValidationResult("Cannot check in for a date in the future");

        }
    }
}