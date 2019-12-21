using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007公共父类
{
    public abstract class Person : EntityBase//公共父类定义为abstract类
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}