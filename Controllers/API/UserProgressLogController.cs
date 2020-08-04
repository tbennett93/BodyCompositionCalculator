using System;
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
        [HttpGet]
        public IHttpActionResult GetUserProgressLogs()
        {
            return Ok(_context.UserProgressLogs.ToList());
        }

        // GET: api/UserProfileLog/5
        [HttpGet]
        public IHttpActionResult GetUserProgressLogs(int id)
        {
            return Ok(_context.UserProgressLogs.Where(u => u.UserProfileId == id).ToList());
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
