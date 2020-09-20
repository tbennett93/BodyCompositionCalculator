using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models.Calculation_Constants;

namespace BodyCompositionCalculator.Models
{
    public static class Calculators
    {
        //Body Composition Calculators
        public static int CalculateBodyFat()
        {
            return 1;
        }

        public static int CalculateMaintenanceCalories(int bmr, double activityValue)
        {
            return Convert.ToInt32(bmr * activityValue);
        }

        public static int CalculateCalorieDeficitFromWeight(double currentWeightInKg, double goalWeightInKg, DateTime startDate, DateTime endDate)
        {
            double amountToLose = (double)(currentWeightInKg - goalWeightInKg);
            double amountToLosePerDay = amountToLose / (endDate - startDate).TotalDays;
            return Convert.ToInt32(amountToLosePerDay * CaloriesPerWeightUnit.CaloriesPerKg);
        }

        public static int CalculateCalorieDeficitFromBodyFat(double currentWeightInKg, int currentBodyFat, int goalBodyFat, DateTime startDate, DateTime endDate)
        {

            double amountToLose = (double)(currentWeightInKg - CalculateEstimatedGoalWeight(currentWeightInKg, currentBodyFat, goalBodyFat));
            double amountToLosePerDay = amountToLose / (endDate - startDate).TotalDays;
            return Convert.ToInt32(amountToLosePerDay * CaloriesPerWeightUnit.CaloriesPerKg);
        }


        public static int CalculateDailyCaloriesFromBodyFat(int bmr, double activityValue, double currentWeightInKg, int currentBodyFat, int goalBodyFat, DateTime startDate, DateTime endDate)
        {
            return CalculateMaintenanceCalories(bmr, activityValue) - CalculateCalorieDeficitFromBodyFat(currentWeightInKg,  currentBodyFat,  goalBodyFat,  startDate,  endDate);
        }

        public static int CalculateDailyCaloriesFromWeight(int bmr, double activityValue, double currentWeightInKg, double goalWeightInKg, DateTime startDate, DateTime endDate)
        {
            return CalculateMaintenanceCalories(bmr, activityValue) - CalculateCalorieDeficitFromWeight( currentWeightInKg,  goalWeightInKg,  startDate,   endDate);
        }

        public static double CalculateEstimatedGoalWeight(double currentWeightInKg, int currentBodyFat, int goalBodyFat )
        {
            double leanBodyMass = currentWeightInKg - (currentWeightInKg * (currentBodyFat / 100.0));
            return leanBodyMass/(1.0-(goalBodyFat/100.0));
        }


        public static int CalculateBmr(double sexModification, int heightInCm, int age, int weightInKg)
        {
            return Convert.ToInt32((10* weightInKg) + (6.25 * heightInCm) - (5 * age) + sexModification);
        }




        //Unit Conversions
            public static double FeetAndInchesToCM(int feet, int inches)
        {
            double cm = (30.48 * feet) + (2.54 * inches);
            return cm;
        }


        public static double InchesToCM(int inches)
        {
            double cm = (double)(2.54 * inches);
            return cm;
        }

        //Unit Conversions
        public static int CmToFt(double cm)
        {
            int Ft = Convert.ToInt32(Math.Floor(cm/30.48));
            return Ft;
        }    

        public static int CmToInches(double cm)
        {
            //Used to get the remaining inches once the feet have been taken off the total cm
            int Inches = Convert.ToInt32(
                //Get the number after the decimal point and multiply by 12 (30.48/2.54 || inches in a foot) to get inches
                ((cm / 30.48)-Math.Floor(cm / 30.48)) * 12
                );
            return Inches;
        }

        public static int KgsToStone(double kg)
        {
            int stone = Convert.ToInt32(Math.Floor(kg / 6.35029318));
            return stone;
        }    

        public static int KgsToLbsRemainingFromStone(double kg)
        {
            //Used to get the remaining inches once the feet have been taken off the total cm
            int lbs = Convert.ToInt32(
                //Get the number after the decimal point and multiply by 12 (30.48/2.54 || inches in a foot) to get inches
                ((kg / 6.35029318) -Math.Floor(kg / 6.35029318)) * 14
                );
            return lbs;
        }

        public static double LbsToKG(double lbs)
        {
            double kg = lbs * 0.45359237;
            return kg;
        }
        public static double StToKg(double st)
        {
            double kg = st * 6.35029318;
            return kg;
        }

        public static double KgsToLbs(double kg)
        {
            double lbs = kg / 0.45359237;
            return lbs;
        }
    }
}