
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AspDotNetMvcCodeFirstProject.Models;
using System.IO;
using System.Data.Entity;

namespace AspDotNetMvcCodeFirstProject.Controllers
{
    public class DonorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Donor
        //public ActionResult Index()
        //{
        //    return View(db.Donors.ToList());
        //}

        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name:_desc" : "";
            var donors = from d in db.Donors
                         select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                donors = donors.Where(d => d.DonorName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name:_desc":
                    donors = donors.OrderByDescending(d=>d.DonorName);
                    break;
                default:
                    donors = donors.OrderBy(d=>d.DonorName);
                    break;
            }
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName");
            return View(donors);
        }

        public ActionResult Create()
        {
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Donor donor)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(donor.UploadImage.FileName);
                string extension = Path.GetExtension(donor.UploadImage.FileName);
                HttpPostedFileBase postedFile = donor.UploadImage;
                int length = postedFile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extension;
                        donor.DonorImage = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        donor.UploadImage.SaveAs(fileName);
                        db.Donors.Add(donor);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Record Inserted Successfully!!')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "Donor");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Record Not Inserted!!')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size Should Be Less Then 1 MB!!')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Image Format Not Supported!!')</script>";
                }
            }
            ViewBag.BloodGroupList = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName", donor.BloodGroupID);
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName");
            return View();
        }

        public ActionResult Edit(int id)
        {
            var DonorRaw = db.Donors.Where(d => d.DonorID == id).FirstOrDefault();
            Session["Image"] = DonorRaw.DonorImage;
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName");
            return View(DonorRaw);
        }

        [HttpPost]
        public ActionResult Edit(Donor donor)
        {
            if (ModelState.IsValid)
            {
                if (donor.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(donor.UploadImage.FileName);
                    string extension = Path.GetExtension(donor.UploadImage.FileName);
                    HttpPostedFileBase postedFile = donor.UploadImage;
                    int length = postedFile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            donor.DonorImage = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            donor.UploadImage.SaveAs(fileName);
                            db.Entry(donor).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["UpdateMessage"] = "<script>alert('Record Updated Successfully!!')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "Donor");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Record Not Updated!!')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size Should Be Less Then 1 MB!!')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Image Format Not Supported!!')</script>";
                    }

                }
                else
                {
                    donor.DonorImage = Session["Image"].ToString();
                    db.Entry(donor).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Record Updated Successfully!!')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Donor");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Record Not Updated!!')</script>";
                    }
                }
            }
            ViewBag.BloodGroupList = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName", donor.BloodGroupID);
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName");
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Donor donor = db.Donors.Find(id);
            db.Donors.Remove(donor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }
    }
}