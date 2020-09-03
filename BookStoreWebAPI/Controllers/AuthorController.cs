using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            
            using (var context = new BookStoresContext()){


                //Get All Authors
                // return context.Author.ToList();  

                // return context.Author.Where(a => a.AuthorId == 1).ToList();

                //Add an Author

                //Author author = new Author();
                //author.FirstName = "Abdullah";

                //author.LastName = "Qureshi";

                //context.Author.Add(author);

                //context.SaveChanges();

                //return context.Author.Where(a => a.FirstName == "Abdullah").ToList();

                //Update an Author
                //Author author = context.Author.Where(a => a.FirstName == "Abdullah").FirstOrDefault();
                //author.Phone = "777-777-7777";

                //context.SaveChanges();

               // return context.Author.Where(a => a.FirstName == "Abdullah").ToList();


                //Delete an Author
                Author author = context.Authors.Where(a => a.FirstName == "Abdullah").FirstOrDefault();
                context.Authors.Remove(author);

                context.SaveChanges();

                return context.Authors.Where(a => a.FirstName == "Abdullah").ToList();


            }





        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
