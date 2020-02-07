using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using WebApplication1.Infrastructure;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {Title = "Books", Version = "v1"}));

            services.AddScoped(x => new AmazonDynamoDBConfig
                                    {ServiceURL = Configuration.GetValue<string>("DynamoDBConnectionString")});
            services.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();
            services.AddScoped<DynamoDBContext>();

            services.AddScoped(x => new AmazonSQSConfig
                                    {ServiceURL = Configuration.GetValue<string>("SQSConnectionString")});
            services.AddScoped<AmazonSQSClient>();
            services.AddScoped<SQSMiddleware>();
            
            services.AddScoped(x => Configuration);

            services.AddScoped<IBooksService, BooksService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllers();
                             });

            app.UseMiddleware<SQSMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DynamoDB"));

            logger.LogInformation(Configuration.GetValue<string>("DynamoDBConnectionString"));
            logger.LogInformation(Configuration.GetValue<string>("SQSConnectionString"));
            logger.LogInformation(Configuration.GetValue<string>("SQSQueueName"));
        }
    }
}
