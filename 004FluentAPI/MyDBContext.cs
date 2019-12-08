using _004FluentAPI2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _004FluentAPI
{
    public class MyDBContext:DbContext 
    {
        public MyDBContext ():base("name=connStr")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly()); 
        }

        public DbSet <Person> Persons { get; set; }
    }
}