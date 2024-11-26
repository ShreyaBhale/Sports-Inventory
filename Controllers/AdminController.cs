using System.Web.Mvc;

namespace SportsStoreManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin Menu
        public ActionResult AdminMenu()
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
        public ActionResult Supplier()
        {
            // Add logic for the Supplier page here
            return View();
        }

        // Redirect to Products page
        public ActionResult Products()
        {
            // Add logic for the Products page here
            return View();
        }
    }
}
