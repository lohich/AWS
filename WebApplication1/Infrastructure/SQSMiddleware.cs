using System;
using System.Threading.Tasks;

using Amazon.SQS;
using Amazon.SQS.Model;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Infrastructure
{
    public class SQSMiddleware : IMiddleware
    {
        private readonly AmazonSQSClient _client;
        private readonly IConfiguration _configuration;

        public SQSMiddleware(AmazonSQSClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var queue = await _client.GetQueueUrlAsync(_configuration.GetValue<string>("SQSQueueName"));
            
            await _client.SendMessageAsync(queue.QueueUrl, context.Request.Path.Value);

            await next(context);
        }
    }
}
