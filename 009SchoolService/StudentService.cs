using SchoolDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SchoolService
{
    public class StudentService
    {
        public void Add(string name, int age, long nationId, long classId)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Student stu = new Student() { Name = name, Age = age, NationId = nationId, ClassId = classId };
                cxt.Students.Add(stu);
                cxt.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Student stu = new Student() { Id = id };
                cxt.Entry(stu).State = System.Data.Entity.EntityState.Deleted;
                cxt.SaveChanges();
            }
        }

        public StudentDTO GetById(long id)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Student stu = cxt.Students.AsNoTracking().Include(s => s.Class).Include(s => s.Nation).SingleOrDefault(s => s.Id == id);
                if (stu == null)
                {
                    return null;
                }
                return new StudentDTO() { Name = stu.Name, Age = stu.Age, ClassName = stu.Class.Name, NationName = stu.Nation.Name };
            }
        }

        public IEnumerable<StudentDTO> GetByClass(long classId)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                IQueryable<Student> students = cxt.Students.AsNoTracking().Include(s => s.Class).Include(s => s.Nation).Where(s => s.ClassId == classId);

                //因为List<T>实现了IEnumerable<T>，所以很自然就会用List<T>作为生成Enumerable<T>的写法(父类出现的地方都可以使用子类替换)
                //List<StudentDTO> list = new List<StudentDTO>();
                //foreach (Student s in students)
                //{
                //    list.Add(new StudentDTO() { Name = s.Name, Age = s.Age, ClassName = s.Class.Name, NationName = s.Nation.Name });
                //}
                //return list;

                //上面代码等价于下面的，使用yield指令
                //yield指令可以告诉编译器函数返回的IEnumber<StudentDTO>由yield return 返回的元素填充
                foreach (Student stu in students)
                {
                    yield return new StudentDTO() { Name = stu.Name, Age = stu.Age, ClassName = stu.Class.Name, NationName = stu.Nation.Name };
                }

            }
        }
    }
}
