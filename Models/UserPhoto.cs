using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace BodyCompositionCalculator.Models
{
    [Table("UserPhotos")]
    public class UserPhoto
    {
        public int Id { get; set; }
        [Column("Photo",TypeName = "varbinary")]
        public byte[] Photo { get; set; }
    }
}