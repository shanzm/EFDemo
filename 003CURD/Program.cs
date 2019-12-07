using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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
                //可以打印EF把查询语句翻译成的SQL语句，方便我们学习探究
                cxt.Database.Log = (sql) => Console.WriteLine(sql);

                //添加数据
                //Insert();

                //查询数据
                //Query();

                //删除数据
                //Delete();

                //更新数据
                //Update();

                //在EF中使用原始的SQL语句
                //UseSqlInEF();

                //不是不是所有lambda 写法都能被EF支持
                #region 说明
                ///有时候你写的linq查询语句逻辑和语法上都是没有问题的
                ///但是不能编译通过，就是EF无法识别,抛异常：NoSupportException
                ///可能是你写的linq太拧巴，换一种写法即可！
                ///举一个例子如下：
                ////var rlt = cxt.Persons.Where(p => Convert.ToString(p.Id) == "3");//逻辑和语法都是没有问题的，对抛异常
                ////你完全可以这样写
                //var rlt = cxt.Persons.Where(p => p.Id == 3);
                //foreach (var item in rlt)
                //{
                //    Console.WriteLine(item.Name);
                //}

                ////EF 中提供了一个 SQLServer 专用的类 SqlFunctions，对于 EF 不支持的函数提供了支持，比如：
                //var rlt2 = cxt.Persons.Where(p => SqlFunctions.DateDiff("hour", p.CreateDateTime, DateTime.Now) > 1);
                ////你可以查看SQLFunction类的定义看看，其中有许多可以使用的函数
                #endregion

                //EF对象状态管理
                //作用：在EF中不用把数据查询出来也可以修改

                //EF对象状态管理--例1--添加数据
                //Add();

                //EF对象状态管理--例2--修改数据--修改Id是36的用户名为"shanzmModified"
                //Modify();

                //EF对象状态管理--例3--查询数据
                //Retrieve();

                //EF对象状态管理--例4--删除数据
                //Delete1();

                //EF 对象状态管理--例5--不查询出来直接删除对象
                //Delete2();

                // EF对象状态管理--例6--EF优化的一个技巧-使用AsNoTracking()
                Optimize();
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        public static void Insert()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Person p1 = new Person() { Name = "003shanzm3", CreateDateTime = DateTime.Now, Age = 35 };
                cxt.Persons.Add(p1);//等价于：cxt.Set<Person>().Add(p1);
                cxt.SaveChanges();//只有SaveChanges()后才会保存到数据库中， //.SaveChanges()返回的受影响的行数
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        public static void Query()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                IQueryable<Person> queryResult = cxt.Persons.Where(n => n.Age > 25).OrderByDescending(n => n.Age);
                //注意返回值的类型是IQueryable<Person>,你也可以写他的父类IEnumberable<Person>,或是直接写var类型推断
                foreach (var p in queryResult)
                {
                    Console.WriteLine($"Id是{p.Id },名字是{p.Name },年龄是{p.Age }");
                }

                //在EF中注意使用Skip（）函数前一定要先使用Orderby（）函数排序
                var query = cxt.Persons.OrderBy(n => n.Id).Skip(2).Take(2);//按照Id排序，跳过2行数据取2行
                foreach (var p in query)
                {
                    Console.WriteLine(p.Id + " " + p.Name);
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public static void Delete()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                ///注意删除数据要先查询，判断是否存在这条数据,一般使用SingleOrDefault(),删除后不要忘记保存
                ///这种查询处数据在删除的方式其实性能较低，但是一般不会出错
                ///因为删除操作较少，所以不考虑性能的情况下使用这种删除方式是还不错的
                try
                {
                    var query1 = cxt.Persons.Where(p => p.Name == "shanzm").SingleOrDefault();
                    //可以这样写：var query1=cxt.Persons.SingleOrDefault(p=>p.Name=="shanzm"
                    if (null != query1)
                    {
                        cxt.Persons.Remove(query1);//若是删除多条数据使用RemoveRange();
                        cxt.SaveChanges();
                        Console.WriteLine($"删除名字为{query1.Name}的用户,成功");
                    }
                    else
                    {
                        Console.WriteLine("数据库中无该用户");
                    }
                }
                catch (InvalidOperationException ex)//因为多条数据会使得函数.SingleOrDefault()引发异常
                {
                    Console.WriteLine(ex.Message);//序列包含一个以上元素
                }
                catch (System.Data.ConstraintException ex)//比如说，正要删除的一条数据的DateTime列为空，则会引发此异常
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public static void Update()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                //依旧是把要更新的对象查询出来，再更新，效率不高，可以sql或是“状态管理”
                var query = cxt.Persons;
                foreach (var p in query)
                {
                    p.CreateDateTime = p.CreateDateTime.AddDays(-1);//日期减一天
                }
                Console.WriteLine(cxt.SaveChanges());
            }

        }

        /// <summary>
        /// 在EF中使用原生SQL
        /// </summary>
        public static void UseSqlInEF()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                ///对于一些批量的修改可以使用SQL语句，性能高
                ///在SQL中为了防止SQL注入，我的SQL语句都是参数化的，而不是拼接字符串
                ///string sql = "insert into T_Persons(Name,CreateDateTime,Age) values(@Name,@CreateDateTime,@Age)";
                ///string name="shanzm";DateTime createDateTime=DateTime.Now;int age=25;
                ///SqlParameter[] param ={
                ///                       new SqlParameter ("@Name",name ),
                ///                       new SqlParameter ("@CreateDateTime",createDateTime ),
                ///                       new  SqlParameter ("@Age",age )
                ///                  };
                ///但是在EF中使用SQL就没必要参数化，因为啊，EF会重新翻译的SQL语句，它会翻译成参数化的SQL语句,所以可以这么写：
                ///string sql = $"insert into T_Persons(Name,CreateDateTime,Age) values({name},{createDateTime},{age})";
                //-------------------在EF中直接使用SQL语句--插入&删除&修改----------------------------------
                cxt.Database.ExecuteSqlCommand("insert into T_Persons(Name ,CreateDateTime,Age) values('shanzmsql',GetDate(),22)");


                //-------------------在EF中直接使用SQL语句--查询--------------------------------------------

                //1.若是你查询的结果是多列则：
                ///首先根据你的查询，新建一个类，类的每一个属性就是你查询结果的列名,然后把查询的结果映射到这个类
                ///所以记得你要明确你想要的查询结果是什么，从而根据结果建类
                ///我们查询每个年龄的人数，所以新建议AgeCount.cs类，添加两个属性：Age和ageCount
                var result = cxt.Database.SqlQuery<AgeCount>("select Age ,count(*) as ageCount from T_Persons group by Age");
                foreach (AgeCount a in result)
                {
                    Console.WriteLine($"年龄是{a.Age }的人数是{a.ageCount }");
                }

                //2.查询结果是一行一列的(就是一个值)，就不需要新建一个结果映射类啦
                int c = cxt.Database.SqlQuery<int>("select count(*) from T_Persons").SingleOrDefault();
                Console.WriteLine($"总人数{c}");

                //3.查询询结果是一列数据的，直接使用此列的数据类型即可
                var queryName = cxt.Database.SqlQuery<string>("select Name from T_Persons");
                foreach (string name in queryName)
                {
                    Console.WriteLine(name);
                }
            }
        }

        /// <summary>
        /// EF对象状态管理-- 例1--添加数据
        /// </summary>
        public static void Add()
        {
            MyDbContext cxt = new MyDbContext();

            Person p1 = new Person() { Name = "shanzm", Age = 24, CreateDateTime = DateTime.Now };
            Console.WriteLine(cxt.Entry(p1).State);//新创建对象是没有被EF监控的，即EntityState值Detached

            cxt.Entry(p1).State = System.Data.Entity.EntityState.Added;// cxt.Persons.Add(p1);
            Console.WriteLine(cxt.Entry(p1).State);//Added

            cxt.SaveChanges();//保存修改到数据库（这里就将上面状态值是Added的对象插入到了数据库），状态值变为：Unchanged
            Console.WriteLine(cxt.Entry(p1).State);
        }

        /// <summary>
        /// EF对象状态管理--例2--修改数据(未查询出来直接修改)--修改Id是36的用户名为"shanzmModified"
        /// </summary>
        public static void Modify()
        {
            MyDbContext cxt = new MyDbContext();//注意虽然EF推荐对DBContext对象使用using释放，但是不用也可以

            Person p1 = new Person() { Id = 36 };
            Console.WriteLine(cxt.Entry(p1).State);//Detached

            cxt.Entry(p1).State = System.Data.Entity.EntityState.Unchanged; //等价于：cxt.Persons.Attach(p1);//注意是Attach（）而不是Add()
            Console.WriteLine(cxt.Entry(p1).State);//Unchanged  //注意理解：Unchanged状态即处于EF监控状态

            p1.Name = "shanzmModified";
            Console.WriteLine(cxt.Entry(p1).State);//Modified

            cxt.SaveChanges();
            Console.WriteLine(cxt.Entry(p1).State);//Unchanged
        }

        /// <summary>
        /// EF对象状态管理-- 例3-查询数据
        /// </summary>
        public static void Retrieve()
        {
            MyDbContext cxt = new MyDbContext();

            var query = cxt.Persons.Where(p => p.Id == 36);
            Person p3 = query.First();//Unchanged  ，注意理解：Unchanged就是处于监控状态
            Console.WriteLine(cxt.Entry(p3).State);
            p3.Age = 110;//Modified 注意这里调试的时候每次进行修改Age值，否则运行一次后，不修改则对象，则状态就是Unchanged啊
            Console.WriteLine(cxt.Entry(p3).State);
            cxt.SaveChanges();
            Console.WriteLine(cxt.Entry(p3).State);//Unchanged
        }

        /// <summary>
        /// EF 对象状态管理--例4--删除对象
        /// </summary>
        public static void Delete1()
        {
            MyDbContext cxt = new MyDbContext();
            Person p4 = cxt.Persons.Where(p => p.Id == 29).First();
            Console.WriteLine(cxt.Entry(p4).State);//Unchanged

            cxt.Entry(p4).State = System.Data.Entity.EntityState.Deleted;//等价于： cxt.Persons.Remove(p4);
            Console.WriteLine(cxt.Entry(p4).State);//Deleted

            cxt.SaveChanges();
            Console.WriteLine(cxt.Entry(p4).State);//Detached
        }

        /// <summary>
        /// EF 对象状态管理--例5--不查询出来直接删除对象
        /// </summary>
        public static void Delete2()
        {
            MyDbContext cxt = new _003CURD.MyDbContext();

            //法1.新建对象为Detached,改为Unchanged,再改为Deleted，保存后即删除指定的对象
            Person p1 = new Person() { Id = 41 };
            cxt.Entry(p1).State = System.Data.Entity.EntityState.Unchanged;
            cxt.Persons.Remove(p1);//等价：cxt.Entry(p1).State = System.Data.Entity.EntityState.Deleted;
            cxt.SaveChanges();

            //法2.新建对象为Detached，直接改为Deleted，保存后即删除指定的对象
            Person p2 = new Person() { Id = 36 };
            cxt.Entry(p2).State = System.Data.Entity.EntityState.Deleted;
            cxt.SaveChanges();

            //注意状态的的改变都是依赖于对象的Id属性的，不可以根据对象的其他属性改变状态，
            //即使你修改也是默认修改的是Id =0的对象（数据库中没有Id=0的数据，所以就会报错）
            //下面就是依据对象的Name属性，删除Name为shanzm的数据，会报错，错误信息中提示没有第0行数据
            //Person p3 = new Person { Name = "shanzm" };
            //cxt.Entry(p3).State = System.Data.Entity.EntityState.Deleted;
            //cxt.SaveChanges();
        }

        /// <summary>
        /// EF对象状态管理--例6--EF优化的一个技巧-使用AsNoTracking()
        /// </summary>
        public static void Optimize()
        {
            //如果查询出来的对象 只是供显示使用，不会修改、删除后保存，那么可以使用AsNoTracking()
            //来使得查询出来的对象是 Detached 状态，这样对对象的修改也还是 Detached状态，
            //即EF不再跟踪这个对象状态的改变，能够提升性能。
           

            MyDbContext cxt = new MyDbContext();
            var p1 = cxt.Persons.First();
            Console.WriteLine(cxt.Entry(p1).State);//Unchanged状态
            p1.Age++;
            Console.WriteLine(cxt.Entry(p1).State);//Modified状态
            cxt.SaveChanges();


            //在查询DbSet<T>对象后，函查询数之前添加一个AsNoTracking()函数，则查询的结果是Detached
            //我们对这个Detected对象的属性修改后也是Detached,但是也因此不能够保存到数据库中，即使你使用了SaveChanges()函数也不会保存
            //因为SaveChanges()函数是根据对象的状态值进行数据操作的，只有状态值是Added、Modified、Deleted的对象才会保存到数据库中
            var p2 = cxt.Persons.AsNoTracking().First();
            Console.WriteLine(cxt.Entry(p2).State);//Detached状态
            p2.Age++;
            Console.WriteLine(cxt.Entry(p2).State);//Detached状态
    
        }
    }

}
