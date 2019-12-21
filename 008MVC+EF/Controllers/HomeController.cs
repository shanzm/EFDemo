using _008EFEntities;
using _008MVC_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _008MVC_EF.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                cxt.Nations.Add(new Nation { Name = "汉" });
                cxt.SaveChanges();
            }

            return Content("OK：测试通过EF新建表，并连接数据库成功！");
        }

        public ActionResult Retrieve()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                #region 查询结果返回的类型
                //注意回忆Linq To Ef 的查询结果是什么类型的，不要偷懒简单的都写完类型推断
                //linq to EF 查询的结果是单个值则类型为该结果的类型
                //若是多列则需建立相应的Model对象（当然一般都是我们定义好的），结果类型为IQueryable<T>类型
                #endregion
                //查询到"三年级二班"
                Class clz = cxt.Classes.Single(c => c.Name == "三年级二班");
                //查询到该班级的所有学生
                IQueryable<Student> students = cxt.Students.Where(s => s.ClassId == clz.Id);
                IEnumerable<Student> stus = students.ToList();



                //注意现在我们有两个实体对象，一个是Class类型的对象，一个是IQueryable<Student>类型的对象
                //我们想要把两个对象中的数据都传递到View中，但是我们知道传递到View的Model只能有一个
                //所以我们要建立一个新的Model类，这个类中要包含Class类型参数和IQueryable<Students>类型参数
                //故：我们建立HomeRetrieveMode.cs类
                //然而我们运行发现，显示DBContext已经被Dispost，其实问题出在哪里啊？
                //cxt.Students.Where(s => s.ClassId == clz.Id);此举的linq查询是延迟查询的，为了立即查询使用ToList（）立即遍历即可
                //故查询的结果类型为IEnumerable<Student> students=cxt.Students.Where(s => s.ClassId == clz.Id).ToList();

                HomeRetrieveModel model = new Models.HomeRetrieveModel() { Class = clz, Students = stus };

                return View(model);
            }
        }
    }
}