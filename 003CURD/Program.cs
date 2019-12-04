using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _003CURD
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext cxt = new MyDbContext())
            {

                cxt.Database.Log = (sql) => Console.WriteLine(sql); //可以显示EF把查询语句翻译成的SQL语句

                #region 添加数据
                //Person p1 = new Person() { Name = "003shanzm3", CreateDateTime = DateTime.Now, Age = 35 };
                //cxt.Persons.Add(p1);//等价于：cxt.Set<Person>().Add(p1);
                //cxt.SaveChanges();//只有SaveChanges()后才会保存到数据库中，
                //.SaveChanges()返回的受影响的行数
                #endregion


                #region 查数据
                IQueryable<Person> queryResult = cxt.Persons.Where(n => n.Age > 25).OrderByDescending(n => n.Age);
                //注意返回值的类型是IQueryable<Person>,你也可以写他的父类IEnumberable<Person>,或是直接写var类型推断
                foreach (var p in queryResult)
                {
                    Console.WriteLine($"Id是{p.Id },名字是{p.Name },年龄是{p.Age }");
                }

                //在EF中注意使用Skip（）函数前一定要先使用Orderby（）函数排序
                //var query = cxt.Persons.OrderBy(n => n.Id).Skip(2).Take(2);//按照Id排序，跳过2行数据取2行
                //foreach (var p in query)
                //{
                //    Console.WriteLine(p.Id + " " + p.Name);
                //}
                #endregion


                #region 删除数据
                //注意删除数据要先查询，判断是否存在这条数据,一般使用SingleOrDefault(),删除后不要忘记保存
                //这种查询处数据在删除的方式其实性能较低，但是一般不会出错
                //因为删除操作较少，所以不考虑性能的情况下使用这种删除方式是还不错的
                //try
                //{
                //    var query1 = cxt.Persons.Where(p => p.Name == "shanzm").SingleOrDefault();
                //    //可以这样写：var query1=cxt.Persons.SingleOrDefault(p=>p.Name=="shanzm"
                //    if (null != query1)
                //    {
                //        cxt.Persons.Remove(query1);//若是删除多条数据使用RemoveRange();
                //        cxt.SaveChanges();
                //        Console.WriteLine($"删除名字为{query1.Name}的用户,成功");
                //    }
                //    else
                //    {
                //        Console.WriteLine("数据库中无该用户");
                //    }
                //}
                //catch (InvalidOperationException ex)//因为多条数据会使得函数.SingleOrDefault()引发异常
                //{
                //    Console.WriteLine(ex.Message);//序列包含一个以上元素
                //}
                //catch (System.Data.ConstraintException ex)//比如说，正要删除的一条数据的DateTime列为空，则会引发此异常
                //{
                //    Console.WriteLine(ex.Message);
                //}
                #endregion

                #region 更新数据
                //依旧是把要更新的对象查询出来，再更新，效率不高，可以sql或是“状态管理”
                //var query = cxt.Persons;
                //foreach (var p in query)
                //{
                //    p.CreateDateTime = p.CreateDateTime.AddDays(1);//日期加一天
                //}
                //Console.WriteLine(cxt.SaveChanges());
                #endregion
                Console.ReadKey();
            }
        }
    }

}
