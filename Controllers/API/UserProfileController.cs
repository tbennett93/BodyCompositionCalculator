using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data.Entity;
using System.Web.Routing;
using BodyCompositionCalculator.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BodyCompositionCalculator.Controllers.API
{
    public class UserProfileController : ApiController
    {


        ApplicationDbContext _context;
        

        public UserProfileController()
        {
            _context = new ApplicationDbContext();
                
        }

        [HttpGet]
        public IHttpActionResult GetUserProfiles()
        {
            return Ok(_context.UserProfiles.ToList());
        }

        //[HttpGet]
        //public UserProfile GetUserProfile()  
        //{
        //    RequestContext.Principal.Identity.GetUserId();
        //    return _context.UserProfiles.SingleOrDefault(u=>u.Id =  RequestContext.Principal.Identity.GetUserId());
        //}


        [HttpPost]

        public IHttpActionResult AddUserProfile(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            _context.SaveChanges();
            return Ok();
        }

    }

}