using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BookStoresContext _context;

        public PublishersController(BookStoresContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        public IEnumerable<Publisher> GetPublishers()
        {
            return _context.Publishers;
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }


        // GET: api/Publishers/5
        [HttpGet("GetPublisherDetails/{id}")]
        public async Task<ActionResult<Publisher>> GetPublisherDetails(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Eager Loading

            //var publisher = _context.Publishers
            //    .Include(pub => pub.Books)
            //    .ThenInclude(books => books.Sales)
            //     .Include(pub => pub.Users)
            //     //.ThenInclude(users => users.)
            //    .Where(pub => pub.PubId == id).FirstOrDefault();

            //Explicit Loading
            var publisher = await _context.Publishers.SingleAsync(pub => pub.PubId == id);

            _context.Entry(publisher)
                .Collection(pub => pub.Users)
                .Query()
                .Where(user => user.EmailAddress.Contains("karin"))
                .Load();


            _context.Entry(publisher)
               .Collection(pub => pub.Books)
               .Query()
               .Include(book => book.Sales)
               .Load();

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }


        // GET: api/Publishers/5
        [HttpGet("PostPublisherDetails")]
        public IActionResult PostPublisherDetails()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = new Publisher
            {
                PublisherName = "Tom Cruise",
                State = "NY",
                Country = "USA",
                City = "New York"
            };

         

            var book1 = new Book
            {
                Title = "Sushi, Anyone?",
                PublishedDate = DateTime.Now
            };

            var book2 = new Book
            {
                Title = "Sushi2, Anyone?",
                PublishedDate = DateTime.Now
            };


            var sales1 = new Sale
            {
                Quantity = 10,
                StoreId = "6380",
                PayTerms = "Net 60",
                OrderNum = "6871",
                OrderDate = DateTime.Now,
            };

            var sales2 = new Sale
            {
                Quantity = 20,
                StoreId = "6380",
                PayTerms = "Net 61",
                OrderNum = "6872",
                OrderDate = DateTime.Now,
            };


            book1.Sales.Add(sales1);
            book2.Sales.Add(sales2);

            publisher.Books.Add(book1);

            publisher.Books.Add(book2);

            _context.Publishers.Add(publisher);

            _context.SaveChanges();


            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }







        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher([FromRoute] int id, [FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Publishers
        [HttpPost]
        public async Task<IActionResult> PostPublisher([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = publisher.PubId }, publisher);
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return Ok(publisher);
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.PubId == id);
        }
    }
}