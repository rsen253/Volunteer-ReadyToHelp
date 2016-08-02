using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class UserPoint
    {
        [Key]
        public string UserId { get; set; }
        public string PointId { get; set; }

        [ForeignKey("PointId")]
        public virtual Point Point { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}