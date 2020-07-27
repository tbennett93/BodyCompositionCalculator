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
        public string Sex { get; set; }
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


        //Body Comp data
        //Make a goal optional but make the goal fields mandatory
        public int? GoalId { get; set; }
        [ForeignKey("GoalId")]
        public Goal Goal { get; set; }
        public double ActivityLevel { get; set; }
        
        //Other
        //public Goal Goal { get; set; }
        public List<UserProgressLog> UserProgressLog { get; set; }
        public Macros Macros { get; set; }

        //Preferences
        [Required]
        public string WeightUnit { get; set; }
        [Required]
        public string HeightUnit { get; set; }




    }
}