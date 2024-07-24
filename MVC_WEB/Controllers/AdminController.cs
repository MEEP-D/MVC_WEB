using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_WEB.Models;  // Thay đổi theo không gian tên của bạn

namespace MVC_WEB.Controllers
{
    [Authorize(Roles = "Admin")]  // Chỉ cho phép quản trị viên truy cập
    public class AdminController : Controller
    {
        private DBSchoolsContext db = new DBSchoolsContext();

        // GET: Admin/Index
        public ActionResult Index()
        {
            var students = db.Students.ToList();
            return View(students);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginViewModel student, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                // Xử lý file ảnh nếu có
                if (photo != null && photo.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(photo.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Photos"), fileName);
                    photo.SaveAs(path);
                    student.PhotoPath = fileName;
                }

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginViewModel student, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(photo.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Photos"), fileName);
                    photo.SaveAs(path);
                    student.PhotoPath = fileName;
                }

                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
