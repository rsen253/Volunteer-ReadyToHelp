using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class Point
    {
        [Key]
        public string PointId { get; set; }
        public string PointImage { get; set; }
    }
}