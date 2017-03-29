﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Safety.Models
{   
    public class TypeTest
    {    
        public int Id { get; set; }
        [Required]
        [Display(Name = "Тип теста")]
        public string Name { get; set; }

        //Ключ для Speciality
        public ICollection<Speciality> Specialitys { get; set; }
        public TypeTest()
        {
            Specialitys = new List<Speciality>();
        }
    }
}
