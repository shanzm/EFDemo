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
            //现在我们是生成了一个关系表T_TeachersStudentRelations，但是哦我们没有建立这张表的实体类
            
            //当然如果中间关系表还想有其他字段，则要必须为中间表建立实体类。
            //比如说：你想要在T_TeachersStudentRelations中添加一列，
            //那么我们就要单独为这个中间表新建一个实体类，类中添加所有的属性作为表的字段
            //这样之后，我们就可以把学生和老师的多对多的关系分为两个一对多的关系：中间表和两个表之间就是一对多的关系
            //建议不要这样做，若是如此就需要哦我们自己维护这样的关系，比较麻烦
            //Undone:不理解！
           
        }
    }
}