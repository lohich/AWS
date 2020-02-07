using System;

using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.DynamoDBv2.DocumentModel;

using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class BooksService : IBooksService
    {
        private DynamoDBContext _client;

        public BooksService(DynamoDBContext client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _client.ScanAsync<Book>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<Book> GetByIsbn(string isbn)
        {
            return await _client.LoadAsync<Book>(isbn);
        }

        public async Task Remove(string isbn)
        {
            await _client.DeleteAsync<Book>(isbn);
        }

        public async Task<Book> Update(string isbn, Book item)
        {
            item.ISBN = isbn;
            await _client.SaveAsync(item);
            return await _client.LoadAsync<Book>(isbn);
        }

        public async Task<Book> Add(Book item)
        {
            await _client.SaveAsync(item);
            return item;
        }
    }
}
