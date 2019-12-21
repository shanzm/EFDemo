using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007公共父类
{
    class Program
    {

        static void Main(string[] args)
        {
            MyDbContext cxt = new MyDbContext();

            //在数据库中插入一些测试数据
            //Insert(cxt);


            CommonCURD<Teacher> com = new CommonCURD<Teacher>(cxt);

            Teacher teacher1 = com.GetById(1);//查询Id=1的老师
            Console.WriteLine($"查询到Id=1的老师是{teacher1.Name}");


            com.MarkDeleted(1);//删除Id=1的老师
            Console.WriteLine($"Id=1的老师已删除，删除时间是{teacher1.DeleteDateTime }");


            IQueryable<Teacher> teachers = com.GetAll(0, 10);
            foreach (Teacher tea in teachers)
            {
                Console.WriteLine($"数据库中所有的teacher{tea.Name}");
            }

            long totalTeacher = com.GetTotalCount();
            Console.WriteLine($"数据库中的老师数量{totalTeacher}");


            Console.WriteLine("OK");
            Console.ReadKey();

        }

        public static void Insert(MyDbContext cxt)
        {
            cxt.Teachers.Add(new Teacher() { Name = "赵老师", Age = 21, Level = 2, Salary = 10000 });
            cxt.Teachers.Add(new Teacher() { Name = "钱老师", Age = 22, Level = 3, Salary = 30000 });
            cxt.Teachers.Add(new Teacher() { Name = "孙老师", Age = 23, Level = 3, Salary = 30000 });
            cxt.Teachers.Add(new Teacher() { Name = "李老师", Age = 24, Level = 3, Salary = 30000 });
            cxt.Teachers.Add(new Teacher() { Name = "周老师", Age = 25, Level = 3, Salary = 30000 });
            cxt.Students.Add(new Student { Name = "张三", Age = 15, StuNo = "1317417001" });
            cxt.Students.Add(new Student { Name = "李四", Age = 15, StuNo = "1317417002" });
            cxt.Students.Add(new Student { Name = "王五", Age = 15, StuNo = "1317417003" });
            cxt.Students.Add(new Student { Name = "赵六", Age = 15, StuNo = "1317417003" });
            cxt.SaveChanges();
        }
    }
}
