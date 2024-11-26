using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStoreManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmployeeMenu()
        {
            return View();
        }

        // Redirect to Sales page
        public ActionResult Sales()
        {
            // Add logic for the Sales page here
            return View();
        }

        // Redirect to Supplier page
        public ActionResult Inventory()
        {
            // Add logic for the Supplier page here
            return View();
        }

        // Redirect to Products page
      
    }
}