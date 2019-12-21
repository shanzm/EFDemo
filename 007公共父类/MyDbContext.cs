using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007公共父类
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=connStr")
        {
            //第一调试的时候，我们让EF自动生成表，注意数据库EFTest中生成了dbo.Teachers和dbo.Students两个表
            //之后我们就使用下面的代码禁止EF在自动生成表
            Database.SetInitializer<MyDbContext>(null);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}