//added
using MVCVechicleLoanProject.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVCVechicleLoanProject.Controllers
{
    public class AdminController : Controller
    {
        private AppDBContext db = new AppDBContext();
        // GET: Admin
        [HttpGet]
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

                    return RedirectToAction("AdminMenu");
                }
                else
                {
                    ViewBag.Message = "Admin not found for given Email and Password";
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult AdminMenu()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerViews(Customers customers)
        {
            return View(db.Customers.ToList());
        }

        [HttpGet]
        public ActionResult LoanViews()
        {
            return View(db.Loans.ToList());
        }



       [HttpGet]
       public ActionResult Edit(int id)
        {
            Loan LoanStatus = db.Loans.FirstOrDefault(ln => ln.CustomerID == id);
            return View(LoanStatus);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Loan loanstatus)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(loanstatus).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Status Update Succesfull";
                return RedirectToAction("CustomerViews");
            }

            return View(loanstatus);
        }


    }
}