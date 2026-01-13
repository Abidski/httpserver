using System.Net;
using System.Net.Sockets;
using System.Text;

namespace httpserver;

public class Program
{
     static async Task<int> Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any, 8080);
        try
        {
            server.Start();
            using var handler = await server.AcceptTcpClientAsync();
            var stream = handler.GetStream();
            var response = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: 12\n\nHello World!";
            var bytes = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(bytes);

        }
        finally
        {
            server.Stop();
        }

        return 1;

    }
}