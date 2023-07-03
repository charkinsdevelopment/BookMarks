using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BookMarksFunction
{
    public class HttpTrigger
    {
        private readonly ILogger _logger;

        public HttpTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        [Function("BookMarks")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var userId = Guid.NewGuid();

            var result = Enumerable.Range(1, 20).Select(index => new BookMark
            {
                UserId = userId,
                Url = "https://www.reddit.com",
                Title = RandomString(10),
                ImageUrl = "https://placehold.it/300x170",
                Description = RandomString(10),
                LastAccessed = DateTime.Now,
                LastUpdated = DateTime.Now,
                Tags = new List<BookMarkTag>(){
                    new(){ 
                        Value = RandomString(12)
                    }
                },
            }).ToArray();

            var response = req.CreateResponse(HttpStatusCode.OK);
            
            response.WriteAsJsonAsync(result);

            return response;
        }

        private static Random random = new Random();

        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
