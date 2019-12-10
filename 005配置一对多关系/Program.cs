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
            //插入数据
            //Insert();

            //在Class类中添加Students属性,是为了什么？
            TestRetrieve();

            Console.WriteLine("OK");
            Console.ReadKey();

        }

        /// <summary>
        /// 在Class类中添加Students属性方便查询
        /// </summary>
        private static void TestRetrieve()
        {
            MyDbContext cxt = new MyDbContext();
            //Class class1 = cxt.Classes.First();
            //foreach (Student s in class1.Students)//查询到具体到班级在遍历其中的学生，在这里使用到了Class类的Students属性
            //{
            //    Console.WriteLine(s.Name);
            //}


            //不推荐在Class类中添加Student集合属性，我们只要在Student类中添加Class属性就够了
            //假设Class类中没有Student集合属性，我可以这样查询:
            //根据DBContext类中Students表查询指定班级的学生
            //即首先查询到指定的班级对象，之后使用班级对象的Id属性，进行查询，即可活的其中的学生
            Class class1 = cxt.Classes.First();
            var students = cxt.Students.Where(s => s.Class.Id  == class1.Id);
            foreach (Student s in students)
            {
                Console.WriteLine(s.Name);
            }

        }

        /// <summary>
        /// 插入数据
        /// </summary>
        private static void Insert()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Class class1 = new Class() { Name = "三年级二班" };
                cxt.Classes.Add(class1);

                Student stu1 = new Student() { Name = "单志铭2", Age = 18, Class = class1 };
                //注意这里我们直接给Student对象的Class属性赋值，
                //而没有给他的ClassId属性赋值，但是EF会自动通过Class属性给ClassId赋值
                cxt.Students.Add(stu1);

                cxt.SaveChanges();

            }
        }
    }
}
