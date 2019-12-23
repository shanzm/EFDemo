using _008EFDTO;
using _008EFEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

///DAL项目需要：
///1.安装EntityFramework
///2.引用008EFEntities
///3.引用008EFDTO

///注意我们这里是调用008EFEntities项目，
///我们把所有的EF相关的东西：实体类、DBContext类、配置类 都写在其中
///但其实全部写在DAL层，也是未尝不可


namespace _008EFDAL
{
    public class StudentDAL
    {
        public StudentDTO GetById(long id)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                //法1
                //Student student = cxt.Students.SingleOrDefault(s => s.Id == id);
                //Class clz = cxt.Classes.SingleOrDefault(c => c.Id == student.ClassId);
                //return new StudentDTO() { Name = student.Name, Age = student.Age, ClassName = clz.Name };

                //法2
                //在linq查询的时候使用了Include，避免关联属性的延迟查询，
                //记住添加命名空间：System.Data.Entity（这里是没有智能提示的，你要记住）
                //因为我们在这里只是查询，所以可以对EF进行优化，禁止跟踪EF状态，使用AsNoTracking()
                Student stu = cxt.Students.AsNoTracking().Include(s => s.Class).SingleOrDefault(s => s.Id == id);
                return new StudentDTO() { Name = stu.Name, Age = stu.Age, ClassName = stu.Class.Name };
                //“ClassName = stu.Class.Name” 就是为了防止这里的延迟加载Class属性，所以哦我们只是使用Include（）避免了延迟加载
            }
        }
    }
}