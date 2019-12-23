using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _008EFDTO
{
    ///注意这个DTO（Data Transfer Object）类，我们是可以随时根据需要传输的数据需要添加属性
    ///其实DTO类就是三层架构中的Model
    ///DTO类是“扁平类”，就是我们里的属性都是简单属性，不会有自定义的类的类型的属性
    ///比如说我这里可能会需要使用Class类中的班级名等Class中的属性，但是我们不会在这里定义一个Class类型的属性
    ///而是直接定义为string 类型的ClassName属性等
    public class StudentDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ClassName { get; set; }
    }
}