using ASP.NETCoreWebAPI.Models;
using MongoDB.Driver;

namespace ASP.NETCoreWebAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(MongoDbService mongoDbService)
        {
            _usersCollection = mongoDbService.GetUserCollection();
        }

        // Kullanıcıyı doğrulama işlemi
        public User Authenticate(string username, string password)
        {
            var user = _usersCollection.Find(u => u.Username == username).FirstOrDefault();
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            return user;
        }

        // Diğer metodlar (Kullanıcı ekleme, güncelleme vb.) burada olabilir
    }
}
