using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007公共父类
{
    public abstract class EntityBase//注意公共父类定义为抽象类
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime DeleteDateTime { get; set; }
    }
}