using SportsInventoryMVC.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace plzRunProj.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Register
        public ActionResult Register()
        {
            populateRoles();
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(SUsers_New model)
        {
            if (ModelState.IsValid)
            {
                populateRoles();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44322/api/");

                    // Post to the API
                    var response = await client.PostAsJsonAsync("UserAPI/Register", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed. Username may already exist.");
                    }
                }
            }

            return View(model);
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(SUsers_New model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44322/api/");

                    var response = await client.PostAsJsonAsync("UserAPI/Login", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var user = await response.Content.ReadAsAsync<SUsers_New>();
                        int? roleId = user.roleid;
                        // Save session or cookie with user info
                        Session["UserId"] = user.id;
                        Session["Username"] = user.username;

                        if (roleId == 1)
                        {
                            return RedirectToAction("AdminMenu", "Admin");
                        }
                        if (roleId == 2)
                        {
                            return RedirectToAction("EmployeeMenu", "Employee");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
            }

            return View(model);
        }

        private void populateRoles()
        {
            SportsInventoryMVCEntities db = new SportsInventoryMVCEntities();

            var roles= db.Roles.ToList();

            ViewBag.roleId = new SelectList(roles, "roleid", "name");
        }
    }
}