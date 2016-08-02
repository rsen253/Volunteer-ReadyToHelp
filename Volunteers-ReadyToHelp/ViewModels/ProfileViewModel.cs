using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.ViewModels
{
    public class ProfileViewModel
    {
        public byte[] UserImage { get; set; }
        public string AvatarId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}