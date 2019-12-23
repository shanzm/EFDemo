using SchoolDTO;
using SchoolService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolWeb.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            StudentService stuService = new StudentService();
            IEnumerable<StudentDTO> students = stuService.GetByClass(1);

            return View(students);
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            NationService ntnService = new NationService();
            IEnumerable<SchoolDTO.NationDTO> nations = ntnService.GetAll();

            //将民族包装为SelectList类型，传到View，提供给Html.DropDownList
            //SelectList slNation = new SelectList(nations, "Id", "Name");
            //使用nameof()函数获取变量名，可以避免打字错误，编译其更容易发现错误
            //第二个参数：下拉列表的value属性,第三个参数：下拉列表的显示属性
            SelectList slNation = new SelectList(nations, nameof(Nation.Id), nameof(Nation.Name));


            //因为传输的数据比较单一，所以不封装为ViewModel类了
            //todo:班级的DropDownlist
            return View(slNation);
        }

        [HttpPost]
        public ActionResult AddNew(string name, int age, long nationId)
        {
            StudentService stuService = new StudentService();
            stuService.Add(name, age, nationId, 1);
            // 重定向
            // return RedirectToAction("Index");使用下面的nameof()，减少自己编码中出现字符串作为参数，可避免不必要的错误
            //也可这么写：return Redirect("/Student/Index");
            return RedirectToAction(nameof(StudentController.Index));
        }

        public ActionResult Delete(long id)
        {
            StudentService stuService = new StudentService();
            stuService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}