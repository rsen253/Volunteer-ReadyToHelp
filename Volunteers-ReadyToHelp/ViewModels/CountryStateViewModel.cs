using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.ViewModels
{
    public class CountryViewModel
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class StateViewModel
    {
        public string CountryId { get; set; }
        public string StateId { get; set; }
        [Display(Name= "State Name")]
        public string StateName { get; set; }
    }
}