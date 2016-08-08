using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class Avatar
    {
        
        public string AvatarId { get; set; }
        public byte[] AvatarData { get; set; }
        public string ExternalLoginUserPictureUrl { get; set; }
    }
}