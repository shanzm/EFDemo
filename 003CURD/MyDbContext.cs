using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _003CURD
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=connStr")
        {
            System.Data.Entity.Database.SetInitializer<MyDbContext>(null);
            //添加此举代码则EF不会帮我们建数据库，因为翻译的SQL语句就清爽许多，便于我们自己调试研究
            //在Program.cs中使用 cxt.Database.Log = (sql) => Console.WriteLine(sql);可以显示EF把查询语句翻译成的SQL语句
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Person> Persons { get; set; }
    }
}