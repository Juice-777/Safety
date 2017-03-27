using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Safety
{   
    public class TypeTest
    {    
        public int Id { get; set; }
        public string Name { get; set; }

        //Ключ для Speciality
        public ICollection<Speciality> Specialitys { get; set; }
        public TypeTest()
        {
            Specialitys = new List<Speciality>();
        }
    }
}
