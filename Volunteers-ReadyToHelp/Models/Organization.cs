using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class Organization
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string logoId { get; set; }
    }
}