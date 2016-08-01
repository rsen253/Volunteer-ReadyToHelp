using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.ViewModels
{
    public class RoleViewModel
    {
        //public int RoleId { get; set; }
        [Required(ErrorMessage= "Role field can't be empty")]
        [Display(Name= "Role Name")]
        public string RoleName { get; set; }
    }
}