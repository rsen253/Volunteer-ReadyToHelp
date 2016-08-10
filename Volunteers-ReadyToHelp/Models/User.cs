using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volunteers_ReadyToHelp.Models
{
    public class CustomUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string RoleId { get; set; }
        public string AvatarId { get; set; }
        //public int ColorId { get; set; }
        public string AboutUser { get; set; }
        

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual Avatar Avataer { get; set; }
       
    }
}