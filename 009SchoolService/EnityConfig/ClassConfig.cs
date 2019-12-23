using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolService.EnityConfig
{
    class ClassConfig:EntityTypeConfiguration<Class>
    {
        public ClassConfig()
        {
            ToTable("T_Classes_009");
            Property(c => c.Name).HasMaxLength(30).IsRequired();
           
        }
    }
}