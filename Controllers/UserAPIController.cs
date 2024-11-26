using SportsInventoryMVC.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace plzRunProj.Controllers
{
    public class UserAPIController : ApiController
    {
        private SportsInventoryMVCEntities db = new SportsInventoryMVCEntities();

        // POST: api/UserAPI/Register
        [HttpPost]
        [Route("api/UserAPI/Register")]
        public IHttpActionResult Register([FromBody]  SUsers_New user )
        {
            if (user == null || string.IsNullOrEmpty(user.username) || string.IsNullOrEmpty(user.password))
            {
                return BadRequest("Invalid data.");
            }

            // Check if the username already exists
            if (db.SUsers_New.Any(u => u.username == user.username))
            {
                return Conflict(); // Username already exists
            }

            db.SUsers_New.Add(user);
            db.SaveChanges();
            return Ok("User registered successfully");
        }

        // POST: api/UserAPI/Login
        [HttpPost]
        [Route("api/UserAPI/Login")]
        public IHttpActionResult Login([FromBody] SUsers_New login)
        {
            var user = db.SUsers_New.FirstOrDefault(u => u.username == login.username && u.password == login.password);

            if (user == null)
            {
                return Unauthorized(); // Invalid username or password
            }

            return Ok(new { username = user.username, roleid = user.roleid }); // Send back the user data or token
        }

    }
}
