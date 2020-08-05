﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BodyCompositionCalculator.Models;

namespace BodyCompositionCalculator.Controllers.API
{
    public class UserProgressLogController : ApiController
    {
        ApplicationDbContext _context;


        public UserProgressLogController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: api/UserProfileLog
        //TODO delete this 'get all logs' before go live
        [HttpGet]
        public IEnumerable<UserProgressLog> GetUserProgressLogs()
        {
            return _context.UserProgressLogs.ToList();
        }

        // GET: api/UserProfileLog/5
        [HttpGet]
        public IHttpActionResult GetUserProgressLogs(int id)
        {
            if(id == Helper_Classes.UserHelpers.GetUserProfile().Id)
                return Ok(_context.UserProgressLogs.Where(u => u.UserProfileId == id).ToList());

            return Unauthorized();
        }

        // POST: api/UserProfileLog
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserProfileLog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserProfileLog/5
        public void Delete(int id)
        {
        }
    }
}
