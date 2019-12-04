using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _001EFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //注意DBContext类是实现IDisposable接口的，所以你要使用using语句释放DBContext对象
            //但是事实上你不使用using语句也是可以的每次用的时候 new MyDbContext对象就行，不用共享同一个实例，共享反而会有问题。
            //SaveChanges() 才会把修改更新到数据库中
            //EF 的开发团队都说要 using DbContext ，很多人不 using ，只是想利用 LazyLoad 而已，但是那样做是违反分层原则的。 
            //建议使用using
            using (TestDbContext ctx = new TestDbContext())
            {
                //主键是自增类型，不用赋值；
                //Age属性是可为空类型，所以可以不赋值
                //其他的属性即使没有标注[Required]特性，你也要给赋值，因为数据库对相应的字段设置为不允许空。
                Person p1 = new Person() { Name = "shanzm", CreateDateTime = DateTime.Now };
                ctx.Persons.Add(p1);//等价于：ctx.Set<Person>().Add(p1);
                ctx.SaveChanges();
            }
        }
    }
}
