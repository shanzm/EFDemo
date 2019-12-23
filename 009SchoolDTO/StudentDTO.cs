using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDTO
{
    public class StudentDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public string NationName { get; set; }
        public long NationId { get; set; }
    }
}
