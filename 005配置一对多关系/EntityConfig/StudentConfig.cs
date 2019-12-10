using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005配置一对多关系.EntityConfig
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            ToTable("T_Students");
            this.HasRequired(s => s.Class).WithMany().HasForeignKey(s => s.ClassId);
            //一个学生必须有一个班级，      一个班级有多个学生，学生表的外键是ClassId

            //1.注意其实哦我们可以不写这些外键配置代码，按照默认的约定的配置完全没问题

            //2.注意一对多的配置，若是单向设计，也就是在Student类中有一个Class属性，我们把配置代码写在“多”的那个类的配置类中，
            //比如说，一个班级有多个学生，则配置代码写在学生类中

            //3.如果 ClassId 可空，那么就要写成 ：
            //this.HasOptional(s => s.Class).WithMany().HasForeignKey(s => s.ClassId);

        }
    }
}