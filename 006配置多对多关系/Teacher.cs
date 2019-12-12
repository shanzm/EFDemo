using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006配置多对多关系
{
    public class Teacher
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}