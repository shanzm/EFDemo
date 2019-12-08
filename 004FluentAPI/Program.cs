using _004FluentAPI;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004FluentAPI2
{
    class Program
    {
        static void Main(string[] args)
        {

            using (MyDBContext cxt = new MyDBContext())
            {
                cxt.Database.Log = (sql) => Console.WriteLine(sql);
                try
                {
                    Person p = new Person() { Age = 60, Name = "mayyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy", CreateDateTime = DateTime.Now };
                    cxt.Persons.Add(p);
                    cxt.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                //注意在EF中若是报错，异常是DbEntityValidationException类型，好像无法从错误详细信息中看到原因，我们可以按照如下方式打印异常信息
                {
                    //foreach (DbEntityValidationResult err in ex.EntityValidationErrors)
                    //{
                    //    foreach (DbValidationError er in err.ValidationErrors)
                    //    {
                    //        Console.WriteLine($"错误对象{er.PropertyName }，错误信息{er.ErrorMessage }");
                    //    }
                    //}

                    //简写为（想想SelectMany（）函数和Select（）函数的区别就是其查询的数据在一个平面上（而非嵌套的））
                    StringBuilder sb = new StringBuilder();
                    foreach (var ve in ex.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
                    {
                        sb.AppendLine(ve.PropertyName + ":" + ve.ErrorMessage);
                    }
                }



                Console.WriteLine("OK");
                Console.ReadKey();
            }


        }
    }
}
