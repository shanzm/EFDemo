using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _008EFEntities
{
    public class Student : EntityBase
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public long ClassId { get; set; }

        public Class Class { get; set; }

        public long NationId { get; set; }
        public Nation Nation { get; set; }
    }
}