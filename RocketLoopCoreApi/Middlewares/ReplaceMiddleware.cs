using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RocketLoopCoreApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace RocketLoopCoreApi.Middlewares
{

    public class ReplaceMiddleware
    {
        private readonly RequestDelegate _next;

        public ReplaceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/users"))
            {
                var request = await FormatRequest(context.Request);                
                var originalBodyStream = context.Response.Body;
                try
                {
                    using (var responseBody = new MemoryStream())
                    {
                        context.Response.Body = responseBody;
                        await _next(context);
                        var response = await FormatResponse(context.Response);

                        // Replace rl with rocketloop in all values of JSON requests
                        response = response.Replace("rl", "rocketloop ");

                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(response, Encoding.UTF8);
                        
                        // TODO: change originalBodyStream async
                        //await responseBody.CopyToAsync(originalBodyStream);
                    }
                    
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            await _next.Invoke(context);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            
            request.EnableRewind();
            
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);            
            string text = await new StreamReader(response.Body).ReadToEndAsync();            
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{text}";
        }
    }
}
