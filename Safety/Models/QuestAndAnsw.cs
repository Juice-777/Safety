using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Safety.Models
{
    public class QuestAndAnsw
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Вопрос")]
        public string Question { get; set; }
        [Required]
        [Display(Name = "Вар. 1")]
        public string Answer1 { get; set; }
        [Required]
        [Display(Name = "Вар. 2")]
        public string Answer2 { get; set; }
        [Display(Name = "Вар. 3")]
        public string Answer3 { get; set; }
        [Display(Name = "Вар. 4")]
        public string Answer4 { get; set; }
        [Required]
        [Display(Name = "Ответ")]
        public int AnswerTrue { get; set; }

        //Ключ для Ticket
        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}