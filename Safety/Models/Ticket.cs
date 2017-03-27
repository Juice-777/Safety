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

        //���� ��� Ticket
        public int? SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

        //���� ��� Question
        public ICollection<Question> Questions { get; set; }
        public Ticket()
        {
            Questions = new List<Question>();
            Results = new List<Result>(); //���� ��� Results

        }

        //���� ��� Results
        public ICollection<Result> Results { get; set; }

    }
}
