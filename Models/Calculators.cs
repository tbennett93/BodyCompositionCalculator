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
            double leanBodyMass = currentWeightInKg - (currentWeightInKg * (currentBodyFat * 100));
            return leanBodyMass/(1-(goalBodyFat/100));
        }


        public static int CalculateBmr(double sexModification, int heightInCm, int age, int weightInKg)
        {
            return Convert.ToInt32((10+ weightInKg) + (6.25 + heightInCm) - (5 * age) + sexModification);
        }




        //Unit Conversions
            public static int FeetAndInchesToCM(int feet, int inches)
        {
            int cm = (int)((30.48 * feet) + (2.54 * inches));
            //TODO this logic
            return cm;
        }


        public static double InchesToCM(int inches)
        {
            double cm = (double)(2.54 * inches);
            //TODO this logic
            return cm;
        }

        public static double LbsToKG(double lbs)
        {
            double kg = lbs / 2.20462;
            return kg;
        }

        public static double KgsToLbs(double kg)
        {
            double lbs = kg * 2.20462;
            return lbs;
        }
    }
}