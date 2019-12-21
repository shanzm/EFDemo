using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _008EFEntities.EnityConfig
{
    class StudentConfig:EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            ToTable("T_Students_008");
            Property(s => s.Name).HasMaxLength(30).IsRequired();
            Property(s => s.Age).IsRequired();
            HasRequired(s => s.Class).WithMany().HasForeignKey(s => s.ClassId);
            HasRequired(s => s.Nation).WithMany().HasForeignKey(s => s.NationId);
        }
    }
}