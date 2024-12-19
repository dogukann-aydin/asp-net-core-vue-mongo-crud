using MongoDB.Bson.Serialization.Attributes;

namespace ASP.NETCoreWebAPI.Models
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }  // Id'yi string olarak tut

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
