using System.Net;
using System.Net.Sockets;
using System.Text;
using httpserver;

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
        try
        {

            using (handler)
            using (var stream = handler.GetStream())
            using (var reader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            {
                await ParseRequest(reader);
                var message = "HTTP/1.1 200 OK\nContent-Type: text/plain; charset=utf-8\nContent-Length: 12\n\nHello World!";
                var dateTimeBytes = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(dateTimeBytes);
         
            }
        }
        catch
        {
            Console.WriteLine("Error in handling client");
        }
    }

    private async Task ParseRequest(StreamReader reader)
    {
        try
        {
            var reqLine = await reader.ReadLineAsync();
            CheckMethod(reqLine);
            
            while (!string.IsNullOrEmpty(reqLine))
            {
                reqLine = await reader.ReadLineAsync();
                CheckHeader(reqLine);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Error in Parse Request");
            Console.WriteLine(e.Message);
        }

    }

    private void CheckMethod(string line)
    {
        string [] words = line.Split(' ');
        switch (words[0])
        {
           case "GET":
               Console.WriteLine("Get method");
               HandleGet(words);
               break;
           case "POST":
               break;
            case "PUT":
               break;
           case "PATCH":
               break;
           case "DELETE":
               break;
        }

    }

    private void CheckHeader(string line)
    {
        
    }

    private void HandleGet(string[] reqLine)
    {
        var method = reqLine[0];
        var uri = reqLine[1];
        var version = reqLine[2];
        var HttpReq = new HttpRequest(method, uri, version);

    }
        
}