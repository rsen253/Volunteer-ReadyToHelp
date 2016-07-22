using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.Models
{
    public class State
    {
        Guid id = Guid.NewGuid();
        [NotMapped]
        public string _stateId { get; set; }
        public string StateId
        {
            get { return _stateId;}
            set { _stateId = id.ToString(); }
        }
        public string CountryId { get; set; }
        public string StateName { get; set; }
        
        
        public virtual Country Country { get; set; }
    }
}