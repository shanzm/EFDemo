using _008EFEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _008MVC_EF.Models
{
    public class HomeRetrieveModel
    {
        public Class Class { get; set; }

        // public IQueryable<Student> Students { get; set; }
        //为何上述写法，不可以，见HomeController中的Retrieve（）中的说明：
        //为时linq立即执行，使用了ToList（）函数，原来的Linq查询结果为IQueryable<Student>变为IEnumerable<Student>
        public IEnumerable<Student> Students { get; set; }

    }
}