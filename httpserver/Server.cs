using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    private TcpListener _listener;
    private bool isRunning;
    public Server()
    {
         _listener = new TcpListener(IPAddress.Any, 8080);
         isRunning = true;
    }

    public async Task Start()
    {
        _listener.Start();
        while (isRunning)
        {
            var handler = await _listener.AcceptTcpClientAsync();
            Task handleClient = Task.Run(() => HandleClient(handler));
        }
    }

    private async Task HandleClient(TcpClient handler)
    {
        var stream = handler.GetStream();
        var response = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: 12\n\nHello World!";
        var bytes = Encoding.UTF8.GetBytes(response);
        await stream.WriteAsync(bytes);
    }
}