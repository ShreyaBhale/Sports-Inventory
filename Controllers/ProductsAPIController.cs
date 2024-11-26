using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SportsInventoryMVC.Models;
namespace plzRunProj.Controllers
{
    public class ProductsAPIController : ApiController
    {
        private SportsInventoryMVCEntities db = new SportsInventoryMVCEntities();

        // GET: api/ProductsAPI
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Product>))]
        public IEnumerable<Product> GetProducts()
        {
            System.Diagnostics.Debug.WriteLine("Hello");
            return db.Products.ToList();
            
            
        }


        // GET: api/ProductsAPI/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/ProductsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductsAPI
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = product.id }, product);
        }

        // DELETE: api/ProductsAPI/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }
        /*
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Category>))]
        public IEnumerable<Category> GetCategories()
        {

            return db.Categories.ToList();


        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Supplier>))]
        public IEnumerable<Supplier> GetSuppliers()
        {

            return db.Suppliers.ToList();


        }
        */


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.id == id) > 0;
        }
    }
}