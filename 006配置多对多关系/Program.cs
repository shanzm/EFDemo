using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006配置多对多关系
{
    class Program
    {
        ///注意调试的时候哦我们是使用EF自动的生成数据库中T_Students2和T_Teachers2和T_TeacherStudentRelations表
        ///所以记住，我们是让他自动的生成表，所以我们数据库中不要事先存在T_Students2和T_Teachers2和T_TeacherStudentRelations表和MigrationHistory等表，
        ///要么全部自己建表，要么全部让他自动生成
        ///
        ///注意，还有若是我们的表结构已经生成了，我们又在实体类中对某个属性进行配置，则运行会抛一场呢：InvalidOperationException
        ///我们在使用EF的时候，自己研究学习的时候可以在Main 函数中添加以下语句，则把之前生成的表删除：
        ///Database.SetInitializer(new DropCreateDatabaseIfModelChanges<XXXContext>());
        ///如果报错“数据库正在使用”，可能是因为开着 Mangement Studio，先关掉就行了。

        ///但是我们可以想到，实际项目中，数据库中若是已经存在了数据，难道我们还要把表删除，重新生成吗
        ///这其实也算是EF的一个麻烦之处，可以使用DBMigration，把数据编写到代码中，可是也还是很麻烦！

        ///做项目的时候建议初期先把主要的类使用 EF 自动生成表，然后在数据库中删除 MigrationHistory 表，
        ///在MyDBContext中添加：Database.SetInitializer<XXXContext>(null); //禁止EF自动生成表
        ///以后对数据库表的修改都手动完成，也就是手动改实体类、手动改数据库表

        static void Main(string[] args)
        {
            //每次运行删除之前自动生成的数据库中的表，再重新生成，记得你要先关闭Mangement Studio
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>());

            MyDbContext cxt = new MyDbContext();

            Student s1 = new Student() { Name = "张三" };
            Student s2 = new Student() { Name = "李四" };
            Student s3 = new Student() { Name = "王五" };

            Teacher t1 = new Teacher() { Name = "王老师" };
            Teacher t2 = new Teacher() { Name = "赵老师" };

            t1.Students.Add(s1);
            t1.Students.Add(s2);
            t1.Students.Add(s3);

            t2.Students.Add(s3);
            //注意多对多的关系我们只要给一方确定其关系即可，不需要两方都添加这样的关系
            //比如说：既然已经从老师方添加了学生，就不需要再学生方添加老师： s3.Teachers.Add(t2);
            //当然你都写也是没有问题的。
            //同样你要接触某个学生和老师的关系，你只要单方面解除即可
            t2.Students.Remove(t2.Students.Single(s => s.Name == "王五"));

            cxt.Teachers.Add(t1);
            cxt.Teachers.Add(t2);



            cxt.SaveChanges();
            Console.WriteLine("OK");

            //注意哦我们只是在cxt.Teachers.Add(t1)和 cxt.Teachers.Add(t2);并没有cxt.Students.Add(...)
            //这和建立关系是一样的，只要单方面的把教师存储，与其关联的学生自动会存储。同样也可以存储学生则与其有关联的老师会自动存储
            var stu = cxt.Students.Single(s => s.Name == "张三");
            Console.WriteLine(stu.Name);
            Console.ReadKey();
        }
    }
}
