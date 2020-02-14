using System;
using System.IO;
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
            if (context.Request.Method != "GET" || context.Request.Method != "DELETE")
            {
                context.Request.EnableBuffering();
                var queue = await _client.GetQueueUrlAsync(_configuration.GetValue<string>("SQSQueueName"));

                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                await _client.SendMessageAsync(queue.QueueUrl, body);
            }

            await next(context);
        }
    }
}
