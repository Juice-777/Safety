using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Safety
{    
    public partial class Answer
    {
        [Key]
        [ForeignKey("Question")]
        public int Id { get; set; }
        public string Version1 { get; set; }
        public string Version2 { get; set; }
        public string Version3 { get; set; }
        public string Version4 { get; set; }
        public string VersionTrue { get; set; }
        
        //Ключ для Question        
        public Question Question { get; set; }
    }
}
