using Safety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Safety.Models
{   
    public class Result
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public System.DateTime Date { get; set; }
        public int Score { get; set; }
        public int SpecialityId { get; set; }

    }
}
