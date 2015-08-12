using System.Linq;
using System.Web.Http;
using WebApi2.Context;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class UserController : ApiController
    {
        readonly AppContext db = new AppContext();

        [HttpGet]
        [Route("api/user/get")]
        public IQueryable<User> GetAllUsers()
        {
            return db.Users;
        }

        [HttpGet]
        [Route("api/user/get/{id}")]
        public User GetUserById(int id)
        {
            return db.Users.Find(id);
        }

        [HttpGet]
        [Route("api/user/getbyusername/{username}")]
        public User GetUserByUsername(string username)
        {
            return db.Users.FirstOrDefault(user => user.Username.ToLower().Equals(username.ToLower()));
        }
        
        [HttpPost]
        [Route("api/user/")]
        public IHttpActionResult Post(User user)
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
        
        [HttpPut]
        [Route("api/user/")]
        public IHttpActionResult Put(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state not valid!");
            }

            var databaseUser = db.Users.ToList().FirstOrDefault(u => u.Username.ToLowerInvariant().Equals(user.Username.ToLowerInvariant()));
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

        [HttpDelete]
        [Route("api/user/{id}")]
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

        public new void Dispose()
        {
            db.Dispose();
            base.Dispose();
        }
    }
}