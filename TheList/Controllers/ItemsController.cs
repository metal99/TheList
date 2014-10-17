using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheList.Models;

namespace TheList.Controllers
{
    public class ItemsController : Controller
    {
        private TheListContext db = new TheListContext();

        // GET: Items
        public ActionResult Index()
        {
            //testing Tuple views for completed and incomplete items
            //dynamic mymodel = new ExpandoObject();
            //mymodel.incomplete = db.Items.Include(i => i.Completed == false).ToList();
            //mymodel.complete = db.Items.Include(i => i.Completed == true).ToList();
            //return View(mymodel);
            return View(db.Items.ToList());
        }


        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TODOItem,Completed")] Item item)
        {

            if (ModelState.IsValid)
            {
                db.Entry(item).Property("Completed").CurrentValue = false;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TODOItem,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            db.Entry(item).Property("Completed").CurrentValue = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Clear the completed list
        public ActionResult ClearList()
        {

            var query = from Items in db.Items
                        where Items.Completed == true
                        select new
                        {
                            Items.ID
                        };
            
            if (query == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in query)
                {
                    int id = item.ID;
                    Item removedItem = db.Items.Find(id);
                    db.Items.Remove(removedItem);
                }
                db.SaveChanges();
            }

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
