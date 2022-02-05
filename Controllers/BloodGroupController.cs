using AspDotNetMvcCodeFirstProject.Models;
using AspDotNetMvcCodeFirstProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspDotNetMvcCodeFirstProject.Controllers
{
    public class BloodGroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BloodGroup
        public ActionResult Index()
        {
            return View(db.BloodGroups.ToList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BloodGroupVM bloodGroupVM)
        {
            BloodGroup bloodGroup = new BloodGroup();
            if (ModelState.IsValid)
            {
                bloodGroup.BloodGroupName = bloodGroupVM.BloodGroupName;
                db.BloodGroups.Add(bloodGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            BloodGroup bloodGroup = db.BloodGroups.Find(id);
            var bloodGroupVM = new BloodGroupVM();
            bloodGroupVM.BloodGroupName = bloodGroup.BloodGroupName;
            return View(bloodGroupVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BloodGroupVM bloodGroupVM, int id)
        {
            BloodGroup bloodGroup = db.BloodGroups.Find(id);
            if (ModelState.IsValid)
            {
                bloodGroup.BloodGroupName = bloodGroupVM.BloodGroupName;
                db.Entry(bloodGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            BloodGroup bloodGroup = db.BloodGroups.SingleOrDefault(b => b.BloodGroupID == id);
            var bloodGroupVM = new BloodGroupVM();
            bloodGroupVM.BloodGroupName = bloodGroup.BloodGroupName;
            return View(bloodGroupVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BloodGroupVM bloodGroupVM, int id)
        {
            BloodGroup bloodGroup = db.BloodGroups.Find(id);
            if (bloodGroup != null)
            {
                db.BloodGroups.Remove(bloodGroup);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            BloodGroup bloodGroup = db.BloodGroups.SingleOrDefault(b => b.BloodGroupID == id);
            var bloodGroupVM = new BloodGroupVM();
            bloodGroupVM.BloodGroupName = bloodGroup.BloodGroupName;
            return View(bloodGroupVM);
        }
    }
}