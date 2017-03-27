using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Safety.Models
{
    public class SafetyContext : DbContext
    {
        public SafetyContext()
            : base("DefaultConnection") { }

        public DbSet<TypeTest> TypeTests { get; set; }
        public DbSet<Speciality> Specialitys { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}