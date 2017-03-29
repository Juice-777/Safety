using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Safety.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "№ билета")]
        public int Nomber { get; set; }

        //Ключ для Speciality
        public int? SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

        //Ключ для Resultsа
        public ICollection<Result> Results { get; set; }
        //Ключ для QuestAndAnsw
        public ICollection<QuestAndAnsw> QuestAndAnsws { get; set; }
        public Ticket()
        {
            Results = new List<Result>();
            QuestAndAnsws = new List<QuestAndAnsw>();
        }
    }
}
