using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class AboutUser
    {
        public string UserId { get; set; }
        public string AboutMe { get; set; }
        public DateTime UserLog { get; set; }
        public string Location { get; set; }
    }
}