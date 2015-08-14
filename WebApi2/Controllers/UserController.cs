using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi2.Context;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class UserController : ApiController
    {
        readonly AppContext db = new AppContext();

        /// <summary>
        /// The GET method that returns the list of users from database.
        /// </summary>
        /// <returns>
        /// The list of users.
        /// </returns>
        public IEnumerable<User> Get()
        {
            return db.Users.ToList();
        }
        
        /// <summary>
        /// Returns a user entity if found.
        /// </summary>
        /// <param name="id">The id of an user.</param>
        /// <returns>The user entity</returns>
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        /// <summary>
        /// The POST method used to create a user entity.
        /// </summary>
        /// <param name="user">The user to add in the database.</param>
        /// <returns>IHttpActionResult that indicates if the operation succeeded or failed</returns>
        public IHttpActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state not valid!");
            }

            if (db.Users.ToList().Any(u => u.Username.ToLowerInvariant().Equals(user.Username.ToLowerInvariant())))
            {
                return BadRequest("Username already exists in the database!");
            }

            db.Users.Add(user);
            db.SaveChanges();

            return Ok($"Added user {user.Username}");
        }
        
        public IHttpActionResult Put(int id, [FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state not valid!");
            }

            var databaseUser = db.Users.ToList().FirstOrDefault(u => u.Id == user.Id);
            if (databaseUser == null)
            {
                return BadRequest("Username not found!");
            }

            databaseUser.Latitude = user.Latitude;
            databaseUser.Longitude = user.Longitude;
            databaseUser.Name = user.Name;
            databaseUser.Password = user.Password;

            db.SaveChanges();
            return Ok($"Updated user {databaseUser.Username}");
        }
        
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state not valid!");
            }

            var databaseUser = db.Users.ToList().FirstOrDefault(u => u.Id.Equals(id));
            if (databaseUser == null)
            {
                return BadRequest("Id not found!");
            }

            db.Users.Remove(databaseUser);
            db.SaveChanges();

            return Ok($"Deleted user {databaseUser.Username}");
        }

        protected new void Dispose()
        {
            db.Dispose();
            base.Dispose();
        }
    }
}