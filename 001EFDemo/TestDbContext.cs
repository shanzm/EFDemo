using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _001EFDemo
{
    class TestDbContext:DbContext
    {
        public TestDbContext() : base("name=connStr")
        {
            //name=connSt 表示使用连接字符串中名字为 connStr 的去连接数据库
        }
        public DbSet<Person> Persons { get; set; }//泛型集合类型
    }
}