using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class LoopbackHttpListener: IDisposable
    {
        const int DefaultTimeout = 60 * 3;
        readonly IWebHost _host;
        readonly TaskCompletionSource<string> _source = new();
        readonly string _url;

        public string Url => _url;

        public LoopbackHttpListener(int port, string path = null)
        {
            path ??= String.Empty;
            if (path.StartsWith("/")) path = path[1..];

            _url = $"http://{IPAddress.Loopback}:{port}/{path}";

            _host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(_url)
                .Configure(Configure)
                .Build();
            _host.Start();
        }

        private void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.Request.Method == "GET")
                {
                    await SetResultAsync(context.Request.QueryString.Value, context);
                }
                else if (context.Request.Method == "POST")
                {
                    if (!context.Request.ContentType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Response.StatusCode = 415;
                    }
                    else
                    {
                        using var sr = new StreamReader(context.Request.Body, Encoding.UTF8);
                        var body = await sr.ReadToEndAsync();
                        await SetResultAsync(body, context);
                    }
                }
                else
                {
                    context.Response.StatusCode = 405;
                }
            });
        }

        private async Task SetResultAsync(string value, HttpContext context)
        {
            try
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<h1>Please return to the app.</h1>");
                await context.Response.Body.FlushAsync();

                _source.TrySetResult(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<h1>Invalid request.</h1>");
                await context.Response.Body.FlushAsync();
            }
        }

        public Task<string> WaitForCallbackAsync(int timeoutInSeconds = DefaultTimeout)
        {
            Task.Run(async () =>
            {
                await Task.Delay(timeoutInSeconds * 1000);
                _source.TrySetCanceled();
            });

            return _source.Task;
        }

        public void Dispose()
        {
            Task.Run(async () =>
            {
                await Task.Delay(500);
                _host.Dispose();
            });
        }
    }
}
