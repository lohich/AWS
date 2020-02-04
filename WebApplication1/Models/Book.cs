using Amazon.DynamoDBv2.DataModel;

namespace WebApplication1.Models
{
    [DynamoDBTable("books")]
    public class Book
    {
        [DynamoDBHashKey]
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
