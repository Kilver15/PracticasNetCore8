using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CacheAPI.Models;
using Microsoft.AspNetCore.OutputCaching;

namespace CacheAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly CacheDbContext _context;
        private readonly IOutputCacheStore _outputCacheStore;

        public PeopleController(CacheDbContext context, IOutputCacheStore outputCacheStore)
        {
            _context = context;
            _outputCacheStore = outputCacheStore;
        }

        // GET: api/People
        [HttpGet]
        [OutputCache(Duration = 30, Tags = ["personas"])]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(long id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Persons.Add(person);
            await _outputCacheStore.EvictByTagAsync("personas", default);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }
    }
}
