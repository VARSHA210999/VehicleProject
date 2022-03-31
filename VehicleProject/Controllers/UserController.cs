using MVCVechicleLoanProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleProject.Models;

namespace VehicleProject.Controllers
{
    public class UserController : Controller
    {
        private AppDBContext db = new AppDBContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "Name,Phone,Email,City,Password")] Customers customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                ViewBag.Message = "Rregistration Succesfull";
                //return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customers CustomerLogin)
        {
            using (AppDBContext db = new AppDBContext())
            {
                var obj = db.Customers.Where(a => a.Email.Equals(CustomerLogin.Email) && a.Password.Equals(CustomerLogin.Password)).FirstOrDefault();

                if (obj != null)
                {
                    Session["UserName"] = obj.Name.ToString();
                    Session["UserID"] = obj.CustomerID.ToString();
                    ViewBag.Message = "Login Succesfull";
                    //return RedirectToAction("Loan", "User");
                }
                else
                {
                    ViewBag.Message = "user not found for given Email and Password";
                    return View();
                }
                return View();
            }
        }

        [HttpGet]
        public ActionResult Loan()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Loan(Loan LoansRegister)
        {
            LoansRegister.CustomerID = Convert.ToInt16(Session["UserID"]);
            if (ModelState.IsValid)
            {
                db.Loans.Add(LoansRegister);
                db.SaveChanges();
                ViewBag.Message = "Loan Apply Succesfull";
                //return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult CheckStatus()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            AppDBContext db = new AppDBContext();
            Loan LoanStatus = db.Loans.FirstOrDefault(ln => ln.CustomerID == id);
            return View(LoanStatus);
        }

    }
}