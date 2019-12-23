using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolService
{
    public class NationService
    {
        //获取所有的民族
        public IEnumerable<Nation> GetAll()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                foreach (Nation n in cxt.Nations)
                {
                    yield return (new Nation() { Id = n.Id, Name = n.Name });
                }
            }
        }
    }
}