using Microsoft.AspNetCore.Mvc;
using ASP.NETCoreWebAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace ASP.NETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserController(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("Users");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Şifreyi SHA256 ile hashle
            using var sha256 = SHA256.Create();
            var hashedPassword = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password))).Replace("-", "").ToLower();

            // MongoDB'den kullanıcıyı kontrol et
            var user = _usersCollection.Find(u => u.Username == request.Username && u.Password == hashedPassword).FirstOrDefault();

            if (user != null)
            {
                return Ok(new { message = "Login successful", userId = user.Id.ToString() });
            }

            return Unauthorized(new { message = "Invalid username or password" });
        }
    }

}
