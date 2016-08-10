using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteers_ReadyToHelp.ServiceLayer
{
    public class ExceptionLog
    {
        [Key]
        public string ErrorId { get; set; }
        public string ErrorType { get; set; }
        public bool IsUserAuthenticate { get; set; }
        public string IP { get; set; }
        public DateTime Date { get; set; }
        public string Browser { get; set; }
    }
}