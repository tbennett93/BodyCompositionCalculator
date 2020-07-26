using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models
{
    public class UserProgressLog
    {
        public int Id { get; set; }
        public int BodyFat { get; set; }
        public double WeightInKgs { get; set; }
        public DateTime Date { get; set; }
        public byte[] Photo { get; set; }
    }
}