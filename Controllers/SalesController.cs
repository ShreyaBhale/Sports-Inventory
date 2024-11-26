using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using SportsInventoryMVC.Models;
namespace SportsInventoryMVC.Controllers
{
    public class SalesController: Controller
    {
        public ActionResult Index()
        {
            IEnumerable<OrderItem> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP GET
                var responseTask = client.GetAsync("SalesAPI");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderItem>>();
                    readTask.Wait();
                    list = readTask.Result;
                }
                else
                {
                    list = Enumerable.Empty<OrderItem>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(list);
        }

        public ActionResult Create()
        {
            populateDropdowns();
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderItem obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var postTask = client.PostAsJsonAsync<OrderItem>("SalesAPI", obj);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            populateDropdowns();
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            OrderItem sale = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var responseTask = client.GetAsync("SalesAPI/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OrderItem>();
                    readTask.Wait();
                    sale = readTask.Result;
                }
            }
            populateDropdowns();
            return View(sale);
        }

        [HttpPost]
        public ActionResult Edit(OrderItem obj)
        {
            populateDropdowns();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP POST
                var putTask = client.PutAsJsonAsync<OrderItem>($"SalesAPI/{obj.orderid}", obj);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Return to Index
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(obj);
        }

        public ActionResult Details(int id)
        {
            // variable to hold the person details retrieved from WebApi
            OrderItem sale = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP GET
                var responseTask = client.GetAsync("SalesAPI?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OrderItem>();
                    readTask.Wait();
                    sale = readTask.Result;
                }
            }
            return View(sale);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP Delete
                var responseTask = client.DeleteAsync("SalesAPI/" + id.ToString());
                responseTask.Wait();
                var deleteTask = responseTask.Result;
                if (deleteTask.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        private void populateDropdowns()
        {
            SportsInventoryMVCEntities db = new SportsInventoryMVCEntities();

            var products = db.Products.ToList();

            ViewBag.productId = new SelectList(products, "id", "name");
        }

    }
}
