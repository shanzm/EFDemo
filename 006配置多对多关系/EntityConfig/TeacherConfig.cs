using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006配置多对多关系.EntityConfig
{
    public class TeacherConfig : EntityTypeConfiguration<Teacher>
    {
        public TeacherConfig()
        {
            ToTable("T_Teachers2");
            this.Property(t => t.Name).HasMaxLength(30).IsRequired();
        }
    }
}