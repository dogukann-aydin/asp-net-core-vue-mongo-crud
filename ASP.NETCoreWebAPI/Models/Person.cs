using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ASP.NETCoreWebAPI.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
