using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Safety
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        //Ключ для Ticket        
        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }

        //Ключ для Answer
        public Answer Answer { get; set; }


    }
}
