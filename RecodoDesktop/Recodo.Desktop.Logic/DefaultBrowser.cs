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
        private readonly AuthorizationOptions _options;
        public DefaultBrowser(AuthorizationOptions options)
        {
            _options = options;
        }

        public async Task<string> InvokeAsync(CancellationToken cancellationToken = default)
        {
            using var listener = new LoopbackHttpListener(_options.RedirectUrl);
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
