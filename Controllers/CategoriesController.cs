﻿using System;
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
    public class CategoriesController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            // Variable to store the list returned by WebApi GetCategorys method
            IEnumerable<Category> list = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP GET
                var responseTask = client.GetAsync("CategoriesApi");  // Categorys is the WebApi controller name
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<IList<Category>>();
                    readTask.Wait();
                    // fill the list vairable created above with the returned result
                    list = readTask.Result;
                }
                else //web api sent error response 
                {
                    list = Enumerable.Empty<Category>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category obj)
        {
            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Category>("CategoriesApi", obj);
                // wait for task to complete
                postTask.Wait();
                // retrieve the result
                var result = postTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            // Add model error
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            // send the view back with model error
            return View(obj);
        }

        
        public ActionResult Edit(int id)
        {
            Category person = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var responseTask = client.GetAsync("CategoriesApi/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();
                    person = readTask.Result;
                }
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Category obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                var putTask = client.PutAsJsonAsync<Category>($"CategoriesApi/{obj.id}", obj);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(obj);
        }

    

        public ActionResult Details(int id)
        {
            // variable to hold the person details retrieved from WebApi
            Category person = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP GET
                var responseTask = client.GetAsync("CategoriesApi?id=" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();
                    // fill the person vairable created above with the returned result
                    person = readTask.Result;
                }
            }
            return View(person);
        }
        public ActionResult Delete(int id)
        {
            // variable to hold the person details retrieved from WebApi
            Category person = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("https://localhost:44322/api/");
                //HTTP Delete
                var responseTask = client.DeleteAsync("CategoriesApi/" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var deleteTask = responseTask.Result;
                // check the status code for success
                if (deleteTask.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

    }
}
