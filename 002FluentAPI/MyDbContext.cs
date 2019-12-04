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

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //获取当前代码所在的程序集，从其中加载所有继承与EntityTypeConfiguration的类到配置中（你可还记得：PersonConfig类就是继承于EntityTypeConfiguration类）
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly ());//注意这里的Assembly对象命名空间是：using System.Reflection;

            //法2
            //删除：modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly ());
            //添加：modelBuilder.Configurations.Add(new PersonConfig());

            //法3
            //若是小项目其实哦我们也可以不单独的写一个PersonConfig.cs类，我们可以在这里这样写：
            //删去：modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly ());
            //添加：modelBuilder.Entity<Person>().ToTable("T_Person");

            //其实你可以注意到，我们若是使用法2或法3，我们是使用到了具体的参数，
            //若是哦我们在团队项目中可能会多人修改这个文件，可能会引起不必要的版本管理上的冲突
            //所以我们还是每个类自定义一个配置文件，在DbContext类中使用获取正在执行的程序集的方式来配置
        }
        public DbSet <Person> Persons { get; set; }

    }
}