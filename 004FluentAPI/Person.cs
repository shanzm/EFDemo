using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004FluentAPI2
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }//注意不在PersonConfig中对其限制，则自动在数据中生成的Name字段的长度是nvarchar(max)
        public DateTime  CreateDateTime { get; set; }
        public int? Age { get   ; set; }
    }
}