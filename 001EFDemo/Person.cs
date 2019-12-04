using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _001EFDemo
{
    #region 说明
    //因为字段名字和属性名字一致，所以不用再特殊指定属性和字段名的对应关系，如果需要特殊指定，则要用[Column("Name")]

    //必填字段标注[Required] 、字段长度[MaxLength(5)] ,外键[foreignKey]
    //可空字段用 int? 
    //如果字段在数据库有默认值，则要在属性上标注[DatabaseGenerated]

    //注意实体类都要写成 public ，否则后面可能会有麻烦。
    #endregion
    [Table("T_Persons")]
    public class Person//将类改为public类
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? Age { get; set; }
    }
}




