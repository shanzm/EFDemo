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

            this.HasMany(t => t.Students).WithMany(s => s.Teachers)
                .Map(m=>m.ToTable ("T_TeachersStudentRelations")
                .MapLeftKey ("TeacherId").MapRightKey ("StudentId"));
            //关系配置到任何一方都可以这样不用中间表建实体（也可以为中间表建立一个实体，其实思路更清晰），就可以完成多对多映射。
            //当然如果中间关系表还想有其他字段，则要必须为中间表建立实体类。
        }
    }
}