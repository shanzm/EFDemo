using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _008EFEntities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreatingDateTime { get; set; } = DateTime.Now;
    }
}