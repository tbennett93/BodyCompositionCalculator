﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyCompositionCalculator.Models;

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
    }
}