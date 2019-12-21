using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007公共父类
{
    class Program
    {
        //注意数据库EFTest中生成了dbo.Teachers和dbo.Students两个表
        static void Main(string[] args)
        {
            MyDbContext cxt = new MyDbContext();

            cxt.Teachers.Add(new Teacher() { Name = "王老师", Age = 28, Level = 2, Salary = 10000 });
            cxt.Teachers.Add(new Teacher() { Name = "章老师", Age = 20, Level = 3, Salary = 30000 });
            cxt.Students.Add(new Student { Name = "张三", Age = 15, StuNo = "1317417" });

            CommonCURD<Teacher> com = new CommonCURD<Teacher>(cxt);

            Teacher teacher1 = com.GetById(1);//查询Id=1的老师
            Console.WriteLine($"查询到Id=1的老师是{teacher1.Name}");


            com.MarkDeleted(1);//删除Id=1的老师
            Console.WriteLine($"Id=1的老师已删除，删除时间是{teacher1.DeleteDateTime }");




            Console.WriteLine("OK");
            Console.ReadKey();

        }
    }
}
