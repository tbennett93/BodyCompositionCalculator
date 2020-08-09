using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models
{
    public class UserProgressLog
    {
        public int Id { get; set; }
        public int? BodyFat { get; set; }
        public double? WeightInKg { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public byte?[] Photo { get; set; }

        public int UserProfileId { get; set; }

        [ForeignKey("UserProfileId")] 
        public UserProfile UserProfile { get; set; }
    }
}