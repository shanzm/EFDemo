using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _008EFDTO
{
    ///注意这个DTO类，我们是可以随时根据需要传输的数据需要添加属性的
   public  class StudentDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ClassName { get; set; }
    }
}