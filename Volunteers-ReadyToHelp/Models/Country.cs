using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class Country
    {
        Guid id = Guid.NewGuid();
        public Country()
        {
            StateList = new List<State>();
        }
        [NotMapped]
        public string _countryId { get; set; }
        public string CountryId
        {
            get { return _countryId;}
            set { _countryId = id.ToString(); }
        }
        public string CountryName { get; set; }

        public virtual ICollection<State> StateList { get; set; }
    }
}