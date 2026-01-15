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
        var httpRequest = new HttpRequest(method, uri, version);
        string body;
        int code;
        
        switch(uri)
        {
            case "/":
                code = 200;
                body = @"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <title>HTTP Server from Scratch</title>
                        <style>
                            body { font-family: Arial, sans-serif; max-width: 800px; margin: 50px auto; padding: 20px; }
                            h1 { color: #333; }
                            .links { margin-top: 20px; }
                            a { display: block; margin: 10px 0; color: #0066cc; text-decoration: none; }
                            a:hover { text-decoration: underline; }
                        </style>
                    </head>
                    <body>
                        <h1>Welcome to the HTTP Server!</h1>
                        <p>This is a simple HTTP server built from scratch using C# and TCP sockets.</p>
                        <div class='links'>
                            <a href='/about'>About Page</a>
                            <a href='/time'>Current Server Time</a>
                            <a href='/json'>JSON Response Example</a>
                        </div>
                    </body>
                    </html>
                        ";
                break;
            case "/about": 
                break;
            default:
                break;
                
            
        }

    }
        
}