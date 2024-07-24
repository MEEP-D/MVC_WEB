using MVC_WEB.Models;
using MVC_WEB.Services; // Đảm bảo import namespace chứa interface IStudentService
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_WEB.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly IStudentService _studentService;
        private DBSchoolsContext db = new DBSchoolsContext();
        public SchoolsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Schools
        public async Task<ActionResult> Index()
        {
            var listSchools = await _studentService.GetStudentsAsync();
            return View(listSchools);
        }

        public async Task<JsonResult> GetStudents()
        {
            var students = await _studentService.GetStudentsAsync();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        // POST: Schools/Create
        [HttpPost]
        public async Task<JsonResult> CreateStudent()
        {
            var file = Request.Files["photo"];
            var student = new LoginViewModel
            {
                LastName = Request.Form["LastName"],
                FirstMidName = Request.Form["FirstMidName"],
                EnrollmentDate = DateTime.Parse(Request.Form["EnrollmentDate"])
            };

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/StudentPhotos"), fileName);
                file.SaveAs(path);
                student.PhotoPath = "/Content/StudentPhotos/" + fileName;
            }

            db.Students.Add(student);
            db.SaveChanges();
            return Json(student);
        }

        // GET: Schools/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            int studentId = int.Parse(Request.Form["ID"]);
            var student = db.Students.Find(studentId);

            if (student != null)
            {
                student.FirstMidName = Request.Form["FirstMidName"];
                student.LastName = Request.Form["LastName"];
                student.EnrollmentDate = DateTime.Parse(Request.Form["EnrollmentDate"]);

                var file = Request.Files["photo"];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/StudentPhotos"), fileName);
                    file.SaveAs(path);
                    student.PhotoPath = "/Content/StudentPhotos/" + fileName;
                }

                db.SaveChanges();
                return Json(student);
            }

            return HttpNotFound();
        }

        // POST: Schools/Edit/5
        [HttpPost]
        public async Task<JsonResult> EditStudent()
        {
            int studentId = int.Parse(Request.Form["ID"]);
            var student = await _studentService.GetStudentByIdAsync(studentId);

            if (student != null)
            {
                student.FirstMidName = Request.Form["FirstMidName"];
                student.LastName = Request.Form["LastName"];
                student.EnrollmentDate = DateTime.Parse(Request.Form["EnrollmentDate"]);

                var file = Request.Files["photo"];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/StudentPhotos"), fileName);
                    file.SaveAs(path);
                    student.PhotoPath = "/Content/StudentPhotos/" + fileName;

                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        student.Photo = binaryReader.ReadBytes(file.ContentLength);
                    }
                }

                var updatedStudent = await _studentService.UpdateStudentAsync(student);
                return Json(updatedStudent);
            }

            return HttpNotFound();
        }

        // GET: Schools/Delete/5
        [HttpPost]
        public async Task<JsonResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Json(new { success = true });
        }

        // GET: Schools/GetPhoto/5
        public ActionResult GetPhoto(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student != null && !string.IsNullOrEmpty(student.PhotoPath))
            {
                var photoPath = Server.MapPath(student.PhotoPath);
                if (System.IO.File.Exists(photoPath))
                {
                    byte[] photo = System.IO.File.ReadAllBytes(photoPath);
                    return File(photo, "image/jpeg");
                }
            }
            return HttpNotFound();
        }
    }
}
