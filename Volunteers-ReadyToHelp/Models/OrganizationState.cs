using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class OrganizationState
    {
        [Key]
        public string OrganizationId { get; set; }
        public bool OrganizationStatus { get; set; }
        public DateTime JoinDate { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }
    }
}