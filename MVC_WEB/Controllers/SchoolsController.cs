using MVC_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WEB.Controllers
{
    public class SchoolsController : Controller
    {
        // GET: Schools
        public ActionResult Index()
        {
            var listSchools = new DBSchoolsContext().Students.ToList();
         
            return View(listSchools);
        }

        // GET: Schools/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schools/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schools/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schools/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
