using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        IAmazonS3 S3Client { get; set; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            S3Client = new AmazonS3Client();
        }

        /// <summary>
        /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
        /// </summary>
        /// <param name="s3Client"></param>
        public Function(IAmazonS3 s3Client)
        {
            this.S3Client = s3Client;
        }
        
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            try
            {
                string fileContent;
                try
                {
                    var file = await S3Client.GetObjectAsync("lohich", "log.txt");
                    var streamReader = new StreamReader(file.ResponseStream);
                    fileContent = await streamReader.ReadToEndAsync();
                }
                catch (AmazonS3Exception)
                {
                    fileContent = "";
                }

                var content = new StringBuilder(fileContent);
                foreach (var item in evnt.Records)
                {
                    content.Append(DateTime.Now);
                    content.Append(" ");
                    content.AppendLine(item.Body);
                }

                await S3Client.PutObjectAsync(new PutObjectRequest {BucketName = "lohich", Key = "log.txt", ContentBody = content.ToString()});

                return null;
            }
            catch(Exception e)
            {
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.StackTrace);
                throw;
            }
        }
    }
}
