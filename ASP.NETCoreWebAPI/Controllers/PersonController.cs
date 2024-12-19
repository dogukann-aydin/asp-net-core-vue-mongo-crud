using Microsoft.AspNetCore.Mvc;
using ASP.NETCoreWebAPI.Models;  // Person sınıfı burada
using ASP.NETCoreWebAPI.Services;  // MongoDbService burada
using MongoDB.Bson;

namespace ASP.NETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public PersonController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // GET: api/person
        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            var persons = await _mongoDbService.GetAsync();
            return Ok(persons);
        }

        // GET: api/person/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(string id)
        {
            var person = await _mongoDbService.GetAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST: api/person
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Person person)
        {
            await _mongoDbService.CreateAsync(person);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        // PUT: api/person/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Person person)
        {
            var existingPerson = await _mongoDbService.GetAsync(id);
            if (existingPerson == null)
            {
                return NotFound();
            }
            await _mongoDbService.UpdateAsync(id, person);
            return NoContent();
        }

        // DELETE: api/person/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var person = await _mongoDbService.GetAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            await _mongoDbService.RemoveAsync(id);
            return NoContent();
        }
    }
}