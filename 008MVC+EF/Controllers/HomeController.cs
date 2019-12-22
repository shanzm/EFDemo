using _008EFBLL;
using _008EFDTO;
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
        ///测试一下于数据库的连接，以及使用EF生成数据库中的表
        public ActionResult Index()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                cxt.Nations.Add(new Nation { Name = "汉" });
                cxt.SaveChanges();
            }

            return Content("OK：测试通过EF新建表，并连接数据库成功！");
        }

        #region 说明：EO、DTO、ViewModel
        ///直接调用EF，查询到数据类型为Entity Object，封装为一个ViewModel传输到View中
        ///实际开发中没有这么操作的，我们要分层，要区分Entity Object 、Data Transfer Object、三层架构中的Model、ViewModel

        ///Entity Object :EO，即实体对象， EF 中的实体类，对 EO 的操作会对数据库产生影响。EO 不应该传递到其他层


        ///Data Transfer Object：DTO ，即数据传输对象，用于在各个层之间传递数据的普通类。
        ///                      DTO有哪些属性取决于其他层要什么数据。DTO 一般是“扁平类”，也就是没有关联属性，都是 普通类型属性。
        ///                      DTO 就是三层 Model，在各个层中间传输数据用的


        /// ViewModel:视图模型，用来组合来自其他层的数据显示到 UI 层。
        ///                    简单的数据可能可以直接把 DTO 交给界面显示，一些复杂的数据可以要从新转换为 ViewModel 对象。
        #endregion
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
                //故：我们建立HomeRetrieveMode.cs类--这就是我们所说的ViewModel
                //然而我们运行发现，显示DBContext已经被Dispost，其实问题出在哪里啊？
                //cxt.Students.Where(s => s.ClassId == clz.Id);此举的linq查询是延迟查询的，为了立即查询使用ToList（）立即遍历即可
                //故查询的结果类型为IEnumerable<Student> students=cxt.Students.Where(s => s.ClassId == clz.Id).ToList();

                HomeRetrieveModel model = new Models.HomeRetrieveModel() { Class = clz, Students = stus };

                return View(model);
            }
        }

        #region 说明：MVC+EF+三层
        ///EFEntity(即EF实体类）、DAL（调用EF）、BLL（调用DAL）、UI(即MVC，Controller调用BLL)
        ///以上除UI（MVC）以外都是类库项目
        ///1.在DAL层中调用EF，对数据库中的数据操作，查询返回的数据封装在DTO中
        ///2.在BLL层调用DAL，数据也封装在DTO中
        ///3.在UI层，MVC中的Controller调用BLL，获取数据为DTO
        ///4.将DTO数据封装为ViewModel传输到MVC中的View中（此步骤省略，直接使用了DTO作为ViewModel）
        #endregion
        public ActionResult Retrieve2()
        {
            ///添加对BLL和DTO的引用
            StudentBLL stuBLL = new StudentBLL();
            StudentDTO stuDTO = stuBLL.GetById(1);//查询Id=1的学生
            //我们可以在MVC的项目中，添加一个相关的ViewModel，把stuDTO中的数据赋予相关的ViewModel中
            //此处。我们直接使用DTO做ViewModel
            return View(stuDTO);
        }
    }
}