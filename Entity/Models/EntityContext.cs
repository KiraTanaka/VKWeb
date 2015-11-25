using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Entity.Models
{
    public class EntityContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Video> PopularVideo { get; set; }
    }
}