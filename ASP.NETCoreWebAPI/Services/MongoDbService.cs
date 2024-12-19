using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ASP.NETCoreWebAPI.Models;

namespace ASP.NETCoreWebAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        // Koleksiyonları almak için genel bir metod ekleyelim
        public IMongoCollection<Person> GetPersonCollection() =>
            _database.GetCollection<Person>("Persons");

        public IMongoCollection<User> GetUserCollection() =>
            _database.GetCollection<User>("Users");

        // Kişi bilgilerini almak için metodlar ekleyebilirsiniz
        public async Task<List<Person>> GetAsync()
        {
            return await GetPersonCollection().Find(person => true).ToListAsync();
        }

        public async Task<Person> GetAsync(string id)
        {
            return await GetPersonCollection().Find(person => person.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Person person)
        {
            await GetPersonCollection().InsertOneAsync(person);
        }

        public async Task UpdateAsync(string id, Person person)
        {
            await GetPersonCollection().ReplaceOneAsync(p => p.Id == id, person);
        }

        public async Task RemoveAsync(string id)
        {
            await GetPersonCollection().DeleteOneAsync(person => person.Id == id);
        }
    }
}
