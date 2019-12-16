using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006配置多对多关系
{
    class Program
    {

        static void Main(string[] args)
        {
            MyDbContext cxt = new MyDbContext();

            Student s1 = new Student() { Name = "张三" };
            Student s2 = new Student() { Name = "李四" };
            Student s3 = new Student() { Name = "王五" };

            Teacher t1 = new Teacher() { Name = "王老师" };
            Teacher t2 = new Teacher() { Name = "赵老师" };

            t1.Students.Add(s1);
            t1.Students.Add(s2);
            t1.Students.Add(s3);

            t2.Students.Add(s3);

            cxt.Teachers.Add(t1);
            cxt.Teachers.Add(t2);
         

            cxt.SaveChanges();
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
