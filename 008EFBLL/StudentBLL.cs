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

    ///注意所有的数据的准备工作都是DAL层准备好，所以不要在BLL层出现和EF数据有关的东西
    ///简单的说就是BLL层不要添加对EF的引用
    public class StudentBLL
    {
        StudentDAL stuDAL = new StudentDAL();
        public StudentDTO GetById(long id)
        {
            return stuDAL.GetById(id);
        }

    }
}