using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Volunteers_ReadyToHelp.Models;

namespace Volunteers_ReadyToHelp.ViewModels
{
    public class ProfileViewModel
    {
        public byte[] UserImage { get; set; }
        public string AvatarId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public string AboutMe { get; set; }
        [Display(Name= "Country")]
        public string CountryId { get; set; }
        [Display(Name= "State")]
        [Required]
        public string StateId { get; set; }
    }
}