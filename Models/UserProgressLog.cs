using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Validation;

namespace BodyCompositionCalculator.Models
{
    public class UserProgressLog
    {
        public int Id { get; set; }
        public int? BodyFat { get; set; }
        public double? WeightInKg { get; set; }
        [CheckInDateNotInFuture]
        public DateTime Date { get; set; } = DateTime.Today;

        [ForeignKey("UserPhotoId")]
        public UserPhoto Photo { get; set; }
        public int? UserPhotoId { get; set; }

        public int UserProfileId { get; set; }

        [ForeignKey("UserProfileId")] 
        public UserProfile UserProfile { get; set; }
    }
}