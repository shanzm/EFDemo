using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005配置一对多关系
{
    public class Class
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        // 在 Class 中配置一个这个属性,最好给这个属性初始化一个对象。注意是 virtual。
        //这样就可以获得所有指向了当前对象的 Stuent 集合，也就是这个班级的所有学生。
        //建议“尽量不要设计双向关系”，即不建议添加这样一个属性
    }
}