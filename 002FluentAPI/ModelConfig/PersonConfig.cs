using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _003CURD.ModelConfig
{
    class PersonConfig : EntityTypeConfiguration<Person>
    {
        public PersonConfig()
        {
            this.ToTable("T_Persons");//映射到T_Perosns表中
                                      //等价于DataAnnotation配置方式中的：[Table("T_Persons")]
        }
    }
}