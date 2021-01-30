using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyCompositionCalculator.Models
{
    public class PhotoManager
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public PhotoManager()
        {

            _context = new ApplicationDbContext();  

        }
        public int UploadProgressPhotoToDb(HttpPostedFileBase photo)
        {
            byte[] uploadedFile = new byte[photo.InputStream.Length];
            photo.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            var newPhoto = new UserPhoto
            {
                Photo = uploadedFile
            };

            _context.UserPhotos.Add(newPhoto);
            _context.SaveChanges();
            return newPhoto.Id;

        }        
        public void UploadProfilePhotoToDb(HttpPostedFileBase photo)
        {
            byte[] uploadedFile = new byte[photo.InputStream.Length];
            photo.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            var newPhoto = new UserPhoto
            {
                Photo = uploadedFile
            };

            var userProfile = Helper_Classes.UserHelpers.GetUserProfile();
            var userProfileId = userProfile.Id;
            var userProfilePhoto = _context.UserPhotos.SingleOrDefault(m => m.Id == userProfile.UserPhotoId);
            _context.UserPhotos.Add(newPhoto);
            _context.SaveChanges();
            _context.UserProfiles.SingleOrDefault(m => m.Id == userProfileId).UserPhotoId = newPhoto.Id;
            _context.SaveChanges();
 

        }

        public void DeletePhotoFromDb(int id)
        {
            var photo = _context.UserPhotos.SingleOrDefault(m => m.Id == id);
            if (photo != null)
            {
                _context.UserPhotos.Remove(photo);
                _context.SaveChanges();
            }
            
        }
    }
}