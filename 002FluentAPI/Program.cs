using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _003CURD
{
    class Program
    {
        #region 调试技巧
        ///调试技巧，若是报错，记得一定要看弹出的错误窗口的下面蓝字：查看详情信息-->InnerException-->Message，
        #endregion

        //001EFDemo项目中我们对Person.cs的配置是直接使用特性标签在类中注明的，称之为DataAnnotations配置方式
        //这种方式比较方便，但是耦合度太高， 一般的类最好是 POCO （Plain Old C# Object） ，
        //也就是没有继承什么特殊的父类，没有标注什么特殊的 Attribute ，没有定义什么特殊的方法 ，就是一堆普通的属性的类

        //所以这里我们演示FluentAPI配置方式
        static void Main(string[] args)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //多个表怎么办？创建多个表的实体类、Config 类，并且在 DbContext 中增加多个DbSet<T> 类型的属性即可

                Person p1 = new Person() { Name = "002shanzm", Age = 18, CreateDateTime = DateTime.Now };
                ctx.Persons.Add(p1);
                ctx.SaveChanges();

            }
        }
    }
}
