using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models;
using BodyCompositionCalculator.Models.Calculation_Constants;

namespace BodyCompositionCalculator.Helper_Classes
{
    public class UserHelpers
    {
            public static string GetUserId()
            {
                var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
                var userId = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                return userId;
            }
            public static string GetUserName()
            {
                var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
                var name = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                return name;
            }

            public static string GetUserMail()
            {
                var identity = (System.Security.Claims.ClaimsPrincipal) System.Threading.Thread.CurrentPrincipal;
                var mail = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Email)
                    .Select(c => c.Value).SingleOrDefault();
                return mail;
            }
            public static UserProfile GetUserProfile()
            {
                var userID = GetUserId();
                return new ApplicationDbContext().UserProfiles.SingleOrDefault(u => u.ApplicationUserId == userID);
            }


            public static string GetHeightUnit()
            {
                var userId = GetUserProfile().Id;
                return new ApplicationDbContext().UserProfiles
                    .Include(m => m.HeightUnit)
                    .Where(m => m.Id == userId)
                    .Select(m => m.HeightUnit.Name)
                    .SingleOrDefault();
            }               
            
            public static string GetWeightUnit()
            {
                var userId = GetUserProfile().Id;
                return new ApplicationDbContext().UserProfiles
                    .Include(m => m.HeightUnit)
                    .Where(m => m.Id == userId)
                    .Select(m => m.WeightUnit.Name)
                    .SingleOrDefault();
            }

            //Used for maximum log date regardless of whether weight/bf null on the date
            public static DateTime GetMaxUserLogDate()
            {
                var today = DateTime.Today;
                var userId = GetUserProfile().Id;
                return new ApplicationDbContext().UserProgressLogs
                    .Where(m => m.UserProfileId == userId && m.Date.Year <= today.Year && m.Date.Month <= today.Month && m.Date.Day <= today.Day)
                    .Select(m => m.Date).OrderByDescending(m => m.Year)
                    .ThenByDescending(m => m.Month)
                    .ThenByDescending(m => m.Day).First();
            }

            //Used for if only max check in where weight or BF populated
            public static DateTime GetMaxUserLogDate(bool calcForBodyFat)
            {
                var today = DateTime.Today;
                var userId = GetUserProfile().Id;
                //If calculating for BF, get max date a BF was recorded, else get max date a weight was recorded
                return calcForBodyFat
                    ? new ApplicationDbContext().UserProgressLogs
                        .Where(m => m.UserProfileId == userId && m.Date.Year <= today.Year &&
                                    m.Date.Month <= today.Month && m.Date.Day <= today.Day && m.BodyFat != null)
                        .Select(m => m.Date).OrderByDescending(m => m.Year)
                        .ThenByDescending(m => m.Month)
                        .ThenByDescending(m => m.Day).First()
                    : new ApplicationDbContext().UserProgressLogs
                        .Where(m => m.UserProfileId == userId && m.Date.Year <= today.Year &&
                                    m.Date.Month <= today.Month && m.Date.Day <= today.Day && m.WeightInKg != null)
                        .Select(m => m.Date).OrderByDescending(m => m.Year)
                        .ThenByDescending(m => m.Month)
                        .ThenByDescending(m => m.Day).First();
            }
            //Used for maximum log date regardless of whether weight/bf null on the date

             public static UserProgressLog GetCurrentCheckIn()
            {
                var userId = GetUserProfile().Id;
                var maxLogDate = GetMaxUserLogDate();
                return new ApplicationDbContext().UserProgressLogs.SingleOrDefault(m =>
                    m.UserProfileId == userId && m.Date == maxLogDate);
            }

        //Used for if only max check in where weight or BF populated

        public static UserProgressLog GetCurrentCheckIn(bool calcForBodyFat)
            {
                var userId = GetUserProfile().Id;
                var maxLogDate = GetMaxUserLogDate(calcForBodyFat);
                return new ApplicationDbContext().
                    UserProgressLogs.SingleOrDefault(m =>
                    m.UserProfileId == userId && m.Date == maxLogDate);
            }



        public static double? GetCurrentWeight()
            {
                return GetCurrentCheckIn(false).WeightInKg;
            }
            public static int? GetCurrentBodyFat()
            {   
                return GetCurrentCheckIn(true).BodyFat;
            }



    }
}