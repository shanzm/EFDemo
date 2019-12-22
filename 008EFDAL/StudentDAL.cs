using _008EFDTO;
using _008EFEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///DAL项目需要：
///1.安装EntityFramework
///2.引用008EFEntities
///3.引用008EFDTO

namespace _008EFDAL
{
    public class StudentDAL
    {
        public StudentDTO GetById(long id)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Student student = cxt.Students.SingleOrDefault(s => s.Id == id);
                Class clz = cxt.Classes.SingleOrDefault(c => c.Id == student.ClassId);

                return new StudentDTO() { Name = student.Name, Age = student.Age, ClassName = clz.Name };
            }
        }
    }
}