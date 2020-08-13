using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BodyCompositionCalculator.Models
{
    public class UserProfile
    {
        //Profile Data
        [Required]
        public int Id { get; set; }
        public byte?[] ProfilePhoto { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
      
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int HeightInCm { get; set; }

        //Create a foreign key to link back to the user
        //This foreign key will be populated with the ApplicationUser Id Pk column value when a new user is submitted by getting the active user id
        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }


        public double ActivityLevel { get; set; }
        

        public List<UserProgressLog> UserProgressLog { get; set; }
        public Macros Macros { get; set; }


        [ForeignKey("WeightUnit")]
        public int WeightUnitId { get; set; }
        public WeightUnit WeightUnit { get; set; }



        [ForeignKey("HeightUnit")]
        public int HeightUnitId { get; set; }
        public HeightUnit HeightUnit { get; set; }


        [ForeignKey("Sex")]
        public int SexId { get; set; }
        public Sex Sex { get; set; }






    }
}