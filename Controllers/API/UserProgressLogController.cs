﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BodyCompositionCalculator.Models;
using BodyCompositionCalculator.Models.Calculation_Constants;
using Microsoft.Ajax.Utilities;

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
       [Authorize]
        public IEnumerable<UserProgressLog> GetUserProgressLogs()
        {

            var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;
            return _context.UserProgressLogs
                .Where(u => u.UserProfileId == userId)
                .OrderBy(u => u.Date)
                .ToList();

        }
    //public IEnumerable<UserProgressLogWithGoal> GetUserProgressLogs()
    //   {  

    //       var userId = Helper_Classes.UserHelpers.GetUserProfile().Id;

    //       var query = from UserProgressLog in _context.UserProgressLogs
    //           join Goal in _context.Goals on UserProgressLog.UserProfileId equals Goal.UserProfileId
    //           orderby UserProgressLog.Date
    //           where Goal.UserProfileId == userId
    //           select new
    //           {
    //               StartDate = Goal.StartDate,
    //               EndDate = Goal.EndDate,
    //               StartWeightInKg = Goal.StartWeightInKg,
    //               TargetWeightInKg = Goal.TargetWeightInKg,
    //               StartBodyFat = Goal.StartBodyFat,
    //               TargetBodyFat = Goal.TargetBodyFat
    //           };

    //       return query.ToList();

    //   }

    // GET: api/UserProfileLog/5
    [NonAction]
    private string ConvertImageToHtml(byte[] photo, int id)
    {
        var dbPhoto =
            Convert.ToBase64String(photo);
        var imgSrc = "'" + String.Format("data:image/jpg;base64,{0}", dbPhoto) + "'";
        var img = new HtmlString(String.Format("<img class='img-thumbnail' src={0}/>", imgSrc));
            //var imgInline = new HtmlString(String.Format("<a href=\"\\Photo\\" + GetPhotoFromLogId(id) + "\" data-toggle=\"popover\" style=\"color: blue\"  data-trigger=\"hover touch\"  data-content=\"{0}\" data-html=\"true\">View</a>", img));
            var imgInline = new HtmlString(String.Format("<a href=\"\\Tracker\\MyPhoto\\" + id + "\" data-toggle=\"popover\" style=\"color: blue\"  data-trigger=\"hover touch\"  data-content=\"{0}\" data-html=\"true\">View</a>", img));

            return imgInline.ToString();
    }

    [HttpGet]
    [Authorize]
    public IHttpActionResult GetUserProgressLogs(int id)
    {
        var weightUnit = Helper_Classes.UserHelpers.GetWeightUnit();
        double weightUnitMultiplier = 1.0;

        if (weightUnit == WeightUnits.Lbs)
            weightUnitMultiplier = 2.20462;

        if (id == Helper_Classes.UserHelpers.GetUserProfile().Id)
        {

            if (weightUnit == WeightUnits.Kg || weightUnit == WeightUnits.Lbs)

                return Ok ((from userProgressLog in _context.UserProgressLogs
                join photo in _context.UserPhotos on userProgressLog.UserPhotoId equals photo.Id into gj
                from subphoto in gj.DefaultIfEmpty()
                where userProgressLog.WeightInKg > 0
                where userProgressLog.UserProfileId == id

                select new
                {
                    ProgressLogId = userProgressLog.Id,
                    userProgressLog.Date,
                    userProgressLog.BodyFat,
                    WeightInKg = Math.Round((double) (userProgressLog.WeightInKg * weightUnitMultiplier)),
                    //Photo = userProgressLog.UserPhotoId,
                    UserPhoto = subphoto.Photo,
                    UserPhotoId = (int?)subphoto.Id

                }).ToList().Select(t => new
            {
                t.ProgressLogId,
                t.Date,
                t.BodyFat,
                t.WeightInKg,
                UserPhoto = t.UserPhoto == null ? null : ConvertImageToHtml(t.UserPhoto, (int)t.UserPhotoId)
                //t.UserPhoto 
            }).ToList());


            if (weightUnit == WeightUnits.LbsAndStone)

                return Ok( (from userProgressLog in _context.UserProgressLogs
                join photo in _context.UserPhotos on userProgressLog.UserPhotoId equals photo.Id into gj
                from subphoto in gj.DefaultIfEmpty()
                where userProgressLog.WeightInKg > 0
                where userProgressLog.UserProfileId == id

                select new
                {
                    ProgressLogId = userProgressLog.Id,
                    userProgressLog.Date,
                    userProgressLog.BodyFat,
                    WeightInKg = Math.Floor(
                                     (double) (userProgressLog.WeightInKg * weightUnitMultiplier) / 6.35029318) +
                                 "st" +
                                 Math.Round((((double) (userProgressLog.WeightInKg * weightUnitMultiplier) / 6.35029318) -
                                  Math.Floor((double) (userProgressLog.WeightInKg * weightUnitMultiplier) /
                                             6.35029318)) * 14) + "lbs",
                    //Photo = userProgressLog.UserPhotoId,
                    //UserPhoto = subphoto.Id,
                    UserPhoto = subphoto.Photo,
                    UserPhotoId = (int?)subphoto.Id

                }).ToList().Select(t => new
            {
                t.ProgressLogId,
                t.Date,
                t.BodyFat,
                t.WeightInKg,
                UserPhoto = t.UserPhoto == null ? null : ConvertImageToHtml(t.UserPhoto, (int)t.UserPhotoId)
                //t.UserPhoto 
            }).ToList());

        }

        return NotFound();
    }

   
    [HttpDelete]
    [Authorize]
    // DELETE: api/UserProfileLog/5
    public void Delete(int id)
    {
        var userProfileId = Helper_Classes.UserHelpers.GetUserProfile().Id;
        //var userCheck = _context.UserProfiles.Include(m=>m.UserProgressLog).SingleOrDefault(m=>m.);
        var progressLogInDb = _context.UserProgressLogs.SingleOrDefault(c => c.Id == id && c.UserProfileId == userProfileId);

        if (progressLogInDb == null)
            throw new HttpResponseException(HttpStatusCode.NotFound);

        var userPhotoId = progressLogInDb.UserPhotoId;



        _context.UserProgressLogs.Remove(progressLogInDb);
        _context.SaveChanges();

        if (userPhotoId != null)
        {
            var photoManager = new PhotoManager();
            photoManager.DeletePhotoFromDb((int)userPhotoId);
            _context.SaveChanges();

        }


    }
    }

}
