using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005配置一对多关系
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Class class1 = new Class() { Name = "三年级二班" };
                cxt.Classes.Add(class1);

                Student stu1 = new Student() { Name = "单志铭2", Age = 18, Class = class1 };//注意这里我们直接给Student对象的Class属性赋值，、
                                                                                           //而没有给他的ClassId属性赋值，但是EF会自动通过Class属性给ClassId赋值
                cxt.Students.Add(stu1);

                cxt.SaveChanges();

            }
            Console.WriteLine("OK");
            Console.ReadKey();

        }
    }
}
