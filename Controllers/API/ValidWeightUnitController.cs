using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BodyCompositionCalculator.Models;

namespace BodyCompositionCalculator.Controllers.API
{
    public class ValidWeightUnitController : ApiController
    {
        private ApplicationDbContext _context;
        public ValidWeightUnitController()
        {
            _context = new ApplicationDbContext();
        }


        //GET api/validweightunit
        //[HttpGet]
        //public IEnumerable<String> GetWeightUnits()
        //{
        //    return _context.WeightUnit.Select(w=>w.WeightUnit);
        //}

        //public IEnumerable<string> GetValidWeightUnits()
        //{

        //    return _context.;

        //}
    }
}
