using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Controller]
    [Route("Books")]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("{isbn}")]
        public async Task<Book> Get(string isbn)
        {
            return await _booksService.GetByIsbn(isbn);
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _booksService.GetAll();
        }

        [HttpDelete("{isbn}")]
        public async void Delete(string isbn)
        {
            await _booksService.Remove(isbn);
        }

        [HttpPost]
        public async Task<Book> Post([FromBody]Book item)
        {
            return await _booksService.Add(item);
        }

        [HttpPut("{isbn}")]
        public async Task<Book> Put(string isbn, [FromBody]Book item)
        {
            return await _booksService.Update(isbn, item);
        }
    }
}
