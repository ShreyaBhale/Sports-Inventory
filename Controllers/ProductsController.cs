using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SportsInventoryMVC.Models;

namespace plzRunProj.Controllers
{
    public class ProductsController : Controller
    {
        public  ActionResult Index()
        {
            IEnumerable<Product> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");

                var responseTask = client.GetAsync("ProductsAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Read and deserialize the response content
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();
                    products = readTask.Result;


                }
                else
                {

                    products = Enumerable.Empty<Product>();
                    ModelState.AddModelError(string.Empty, "Failed to connect to server!");
                }
            }
            

            return View(products);

        }

    public ActionResult Create()
        {
            populateDropdowns();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var postTask = client.PostAsJsonAsync<Product>("ProductsAPI", product);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Server Error!");
            populateDropdowns() ;
            return View(product);
        }


        public ActionResult Edit(int id)
        {
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var putTask = client.GetAsync("ProductsAPI/" + id.ToString());
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();
                    product = readTask.Result;
                   
                }
            }
            populateDropdowns();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            populateDropdowns();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var putTask = client.PutAsJsonAsync<Product>($"ProductsAPI/{product.id}", product);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }
            
            return View(product);
        }

        public ActionResult Details(int id)
        {
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var responseTask = client.GetAsync("ProductsAPI?id=" + id.ToString());

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();
                    product = readTask.Result;
                }
            }
            return View(product);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");

                var responseTask = client.DeleteAsync("ProductsAPI/" + id.ToString());

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

            var categories= db.Categories.ToList();
            var suppliers=db.Suppliers.ToList();

            ViewBag.CategoryId = new SelectList(categories, "id", "name");
            ViewBag.SupplierId= new SelectList(suppliers, "id", "name");
        }

       
    }
}
