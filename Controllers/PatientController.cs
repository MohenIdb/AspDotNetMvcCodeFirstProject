
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AspDotNetMvcCodeFirstProject.Models;

namespace AspDotNetMvcCodeFirstProject.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: Patient
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        public ActionResult Add()
        {
            //return View();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(Patient patient)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(patient.UploadImg.FileName);
                string extension = Path.GetExtension(patient.UploadImg.FileName);
                HttpPostedFileBase postedFile = patient.UploadImg;
                int length = postedFile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extension;
                        patient.PatientImage = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        patient.UploadImg.SaveAs(fileName);
                        db.Patients.Add(patient);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Record Inserted Successfully!!')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "Patient");
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
            
            //return View();
            return PartialView("_PatientDetail", db.Patients.ToList());
        }

        //public ActionResult Edit(int id)
        //{
        //    var PatientRaw = db.Patients.Where(p => p.PatientID == id).FirstOrDefault();
        //    Session["Image"] = PatientRaw.PatientImage;
        //    //return View(PatientRaw);
        //    return PartialView();
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Patient patient = db.Patients.Find(id);
            Session["Image"] = patient.PatientImage;
            if (patient == null)
            {
                HttpNotFound();
            }
            //return View(patient);
            return PartialView(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                if (patient.UploadImg != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(patient.UploadImg.FileName);
                    string extension = Path.GetExtension(patient.UploadImg.FileName);
                    HttpPostedFileBase postedFile = patient.UploadImg;
                    int length = postedFile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            patient.PatientImage = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            patient.UploadImg.SaveAs(fileName);
                            db.Entry(patient).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["UpdateMessage"] = "<script>alert('Record Updated Successfully!!')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "Patient");
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
                    patient.PatientImage = Session["Image"].ToString();
                    db.Entry(patient).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Record Updated Successfully!!')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Record Not Updated!!')</script>";
                    }
                }
            }
            //return View();
            return PartialView("_PatientDetail", db.Patients.ToList());
        }

        public ActionResult Delete(int id)
        {
            db.Patients.Remove(db.Patients.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

    }
}