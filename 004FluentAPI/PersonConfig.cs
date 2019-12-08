using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004FluentAPI2
{
    class PersonConfig : EntityTypeConfiguration<Person>
    {
        public PersonConfig()
        {
            //注意一开始在数据库中是没有T_Persons2这张表的，我们使用EF自动生成
            //注意会自动同时生成一个__MigrationHistory表，其中存储的是EF自动生成的表的信息
            this.ToTable("T_Persons2");//映射到T_Perosns表中
                                       //等价于DataAnnotation配置方式中的：[Table("T_Persons")]

            this.Property(p => p.Name).HasMaxLength(30).IsRequired();//设置Name属性的最大长度为30且不可为空
            this.Property(p => p.Age).IsOptional();//设置Age属性可空

            
            //如果属性属性值是引用类型，不允许为空的时候设置 IsRequired()。
            //对于Age我们在Person类的定义的时候，就是定义为int?,即可空，所以其实我们没必要在这里再特别设置为.IsOption()

            //注意这里我们就要理解：若是我们自己在数据库中建一个表T_Perosns2,同时设置Name字段的长度为30
            //这和在PersonCongfig文件中的Name字段的长度设置为30有何区别吗？
            //依赖于数据库的“字段长度、是否为空”等的约束是在数据提交到数据库服务器的时候才会检查
            //EF的配置，则是由 EF 来检查的，如果检查出错，根本不会被提交给服务器。


            //EF 默认规则是“主键属性不允许为空，引用类型允许为空，可空的值类型 long? 等允许为空，值类型不允许为空。”
            //所以对于值类型我们不需要进行配置，可空我们自然定义为可空的值类型，如int?,不可空的值类型，EF就默认为不可为空
            //只需要对不可为空的引用类型进行配置，添加一个.IsRequired();
        }
    }
}