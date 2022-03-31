using MVCVechicleLoanProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleProject.Models;

namespace VehicleProject.Controllers
{
    public class AdminController : Controller
    {
        private AppDBContext db = new AppDBContext();
        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(Admin admin)
        {
            using (AppDBContext db = new AppDBContext())
            {
                var obj = db.Admins.Where(a => a.UserName.Equals(admin.UserName)
                                && a.Passsword.Equals(admin.Passsword)).FirstOrDefault();

                if (obj != null)
                {
                    Session["UserName"] = obj.UserName.ToString();
                    ViewBag.Message = "Login Successfull";
                    

                   
                }
                else
                {
                    ViewBag.Message = "Admin not found for given Email and Password";
                    return View();
                }
                return RedirectToAction("AdminMenu");
            }
        }

        [HttpGet]
        public ActionResult AdminMenu()
        {
            return View();
        }

        public ActionResult LoanStatus()
        {
            return View(db.Loans.ToList());

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loans = db.Loans.Find(id);
            if (loans == null)
            {
                return HttpNotFound();
            }
            return View(loans);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.Status = "Approved";
                
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoanStatus");
            }
            return View(loan);
        }

        public ActionResult Decline(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loans = db.Loans.Find(id);
            if (loans == null)
            {
                return HttpNotFound();
            }
            return View(loans);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Decline(Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.Status = "Declined";
                
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoanStatus");
            }
            return View(loan);
        }

        [HttpGet]
        public ActionResult CustomerViews()
        {
            return View(db.Customers.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
            db.SaveChanges();
/*
            Loan loans = db.Loans.Where(s => s.CustomerID == id).FirstOrDefault();
            db.Loans.Remove(loans);
            db.SaveChanges();*/

            return RedirectToAction("CustomerViews");
        }

    }
}