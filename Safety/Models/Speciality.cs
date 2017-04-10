using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Safety.Models
{ 
    public class Speciality
    {    
        public int Id { get; set; }
        [Required]
        [Display(Name = "Специальность")]
        public string Name { get; set; }

        //Ключ для TypeTest
        public int? TypeTestId { get; set; }
        public TypeTest TypeTest { get; set; }

        //Ключ для Result
        public ICollection<Result> Results { get; set; }

        //Ключ для Ticket
        public ICollection<Ticket> Tickets { get; set; }
        public Speciality()
        {
            Tickets = new List<Ticket>();
            Results = new List<Result>();
        }
    }
}
