using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class DefaultBrowser
    {
        public int Port { get; }
        private readonly string _path;
        private readonly AuthRequestOptions _options;
        public DefaultBrowser(AuthRequestOptions options, int? port = null, string path = null)
        {
            _path = path;

            if (!port.HasValue)
            {
                Port = GetRandomUnusedPort();
            }
            else
            {
                Port = port.Value;
            }
            _options = options;
            _options.RedirectUrl = $"http://{IPAddress.Loopback}:{Port}/";
        }

        public async Task<string> InvokeAsync(CancellationToken cancellationToken = default)
        {
            using var listener = new LoopbackHttpListener(Port, _path);
            Open(_options.GetAuthRequestUrl());

            try
            {
                var result = await listener.WaitForCallbackAsync();
                if (string.IsNullOrWhiteSpace(result))
                {
                    return "Empty response.";
                }

                return result;
            }
            catch (TaskCanceledException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        public static void Open(string url)
        {
            try 
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
