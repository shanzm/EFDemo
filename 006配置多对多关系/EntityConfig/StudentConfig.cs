using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006配置多对多关系.EntityConfig
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            ToTable("T_Students2");
            this.Property(s => s.Name).HasMaxLength(30).IsRequired();
        }
    }
}