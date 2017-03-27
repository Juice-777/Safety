using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Safety
{
    
    public partial class Speciality
    {    
        public int Id { get; set; }
        public string Name { get; set; }

        //���� ��� TypeTest
        public int? TypeTestId { get; set; }
        public TypeTest TypeTest { get; set; }

        //���� ��� Ticket
        public ICollection<Ticket> Tickets { get; set; }
        public Speciality()
        {
            Tickets = new List<Ticket>();
        }
    }
}
