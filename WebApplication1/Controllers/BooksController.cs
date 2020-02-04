using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Controller]
    [Route("Books")]
    public class BooksController : Controller
    {
        [HttpGet("{isbn}")]
        public Book Get(string isbn)
        {
            return new Book();
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return Enumerable.Empty<Book>();
        }

        [HttpDelete("{isbn}")]
        public void Delete(string isbn)
        {
            
        }

        [HttpPost]
        public Book Post([FromBody]Book item)
        {
            return new Book();
        }

        [HttpPut("{isbn}")]
        public Book Get(string isbn, [FromBody]Book item)
        {
            return new Book();
        }
    }
}
