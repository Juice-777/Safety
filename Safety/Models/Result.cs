using Safety.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Safety.Models
{   
    public class Result
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Дата сдачи")]
        public System.DateTime Date { get; set; }
        [Display(Name = "Получено баллов")]
        public int Score { get; set; }
        [Display(Name = "Макс балл")]
        public int MaxScore { get; set; }
        [Display(Name = "Специальность")]

        //Ключ для Speciality
        public int? SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

    }
}
