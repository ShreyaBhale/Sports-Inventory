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

namespace SportsInventoryMVC.Controllers
{
    public class ProcurementsAPIController : ApiController
    {
        private SportsInventoryMVCEntities db = new SportsInventoryMVCEntities();

        // GET: api/ProcurementsAPI
        public IEnumerable<procurementOrderItem> GetprocurementOrderItems()
        {
            return db.procurementOrderItems.ToList();
        }

        // GET: api/ProcurementsAPI/5
        [ResponseType(typeof(procurementOrderItem))]
        public IHttpActionResult GetprocurementOrderItem(int id)
        {
            procurementOrderItem procurementOrderItem = db.procurementOrderItems.Find(id);
            if (procurementOrderItem == null)
            {
                return NotFound();
            }

            return Ok(procurementOrderItem);
        }

        // PUT: api/ProcurementsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutprocurementOrderItem(int id, procurementOrderItem procurementOrderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procurementOrderItem.id)
            {
                return BadRequest();
            }

            db.Entry(procurementOrderItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!procurementOrderItemExists(id))
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

        // POST: api/ProcurementsAPI
        [ResponseType(typeof(procurementOrderItem))]
        public IHttpActionResult PostprocurementOrderItem(procurementOrderItem procurementOrderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.procurementOrderItems.Add(procurementOrderItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (procurementOrderItemExists(procurementOrderItem.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = procurementOrderItem.id }, procurementOrderItem);
        }

        // DELETE: api/ProcurementsAPI/5
        [ResponseType(typeof(procurementOrderItem))]
        public IHttpActionResult DeleteprocurementOrderItem(int id)
        {
            procurementOrderItem procurementOrderItem = db.procurementOrderItems.Find(id);
            if (procurementOrderItem == null)
            {
                return NotFound();
            }

            db.procurementOrderItems.Remove(procurementOrderItem);
            db.SaveChanges();

            return Ok(procurementOrderItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool procurementOrderItemExists(int id)
        {
            return db.procurementOrderItems.Count(e => e.id == id) > 0;
        }
    }
}