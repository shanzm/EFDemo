using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005配置一对多关系
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long  ClassId { get; set; }
        public int Age { get; set; }

        public virtual  Class Class { get; set; }//通过Student类中添加这样一个Class类型的属性就将Student和Class类联系起来了
    }
}