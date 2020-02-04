using System.Collections.Generic;
using System.Threading.Tasks;

using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetByIsbn(string isbn);
        Task Remove(string isbn);
        Task<Book> Update(string isbn, Book item);
        Task<Book> Add(Book item);
    }
}
