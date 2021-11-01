using Microsoft.AspNet.Identity;
using OnlineToDoList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoList
        public ActionResult Index()
        {
            return View();
        }
        private IEnumerable<ToDoViewModel> GetToDoList()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            IEnumerable<ToDoViewModel> toDoList = db.tasks.ToList().Where(x => x.User == currentUser);

            int taskCompletedCount = 0;
            foreach (ToDoViewModel item in toDoList)
            {
                if (item.IsDone)
                {
                    taskCompletedCount++;
                }
            }
            ViewBag.percentageCompleted = Math.Round(100f * ((float)taskCompletedCount / (float)toDoList.Count()));
            return toDoList;
        }
        public ActionResult BuildToDoTable()
        {
            return PartialView("_ToDoTable", GetToDoList());
        }

        // GET: ToDoList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoViewModel toDoViewModel = db.tasks.Find(id);
            if (toDoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(toDoViewModel);
        }

        // GET: ToDoList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoViewModel toDoViewModel)
        {
            if (ModelState.IsValid)
            {
                toDoViewModel.ModifiedDate = System.DateTime.Now;
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                toDoViewModel.User = currentUser;
                db.tasks.Add(toDoViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDoViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate(ToDoViewModel toDoViewModel)
        {
            if (ModelState.IsValid)
            {
                toDoViewModel.ModifiedDate = System.DateTime.Now;
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                toDoViewModel.User = currentUser;
                toDoViewModel.IsDone = false;
                db.tasks.Add(toDoViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView("_ToDoTable", GetToDoList());
        }

        // GET: ToDoList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoViewModel toDoViewModel = db.tasks.Find(id);
            if (toDoViewModel == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (toDoViewModel.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(toDoViewModel);
        }

        // POST: ToDoList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,ModifiedDate,IsDone")] ToDoViewModel toDoViewModel)
        {
            if (ModelState.IsValid)
            {
                toDoViewModel.ModifiedDate = System.DateTime.Now;
                db.Entry(toDoViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoViewModel);
        }
        [HttpPost]
        public ActionResult AjaxEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoViewModel toDoViewModel = db.tasks.Find(id);
            if (toDoViewModel == null)
            {
                return HttpNotFound();
            }
            else
            {
                toDoViewModel.ModifiedDate = System.DateTime.UtcNow;
                toDoViewModel.IsDone = value;
                db.Entry(toDoViewModel).State = EntityState.Modified;
                db.SaveChanges();
            }
            return PartialView("_ToDoTable", GetToDoList());

        }
        // GET: ToDoList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoViewModel toDoViewModel = db.tasks.Find(id);
            if (toDoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(toDoViewModel);
        }

        // POST: ToDoList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoViewModel toDoViewModel = db.tasks.Find(id);
            db.tasks.Remove(toDoViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
