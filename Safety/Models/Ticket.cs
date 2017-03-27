using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Safety
{
    public  class Ticket
    {    
        public int Id { get; set; }
        public int Nomber { get; set; }

        //Ключ для Ticket
        public int? SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

        //Ключ для Question
        public ICollection<Question> Questions { get; set; }
        public Ticket()
        {
            Questions = new List<Question>();
            Results = new List<Result>(); //Ключ для Results

        }

        //Ключ для Results
        public ICollection<Result> Results { get; set; }

    }
}
