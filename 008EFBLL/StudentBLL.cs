using _008EFDAL;
using _008EFDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


///BLL项目需要
///添加对008EFDAL的引用
///添加对008EFDTO的引用

namespace _008EFBLL
{
    public class StudentBLL
    {
        StudentDAL stuDAL = new StudentDAL();
        public StudentDTO GetById(long id)
        {
            return stuDAL.GetById(id);
        }

    }
}