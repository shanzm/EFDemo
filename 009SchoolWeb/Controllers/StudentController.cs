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
    }
}