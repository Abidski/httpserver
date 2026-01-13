using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace httpserver;

public class Program
{
     static async Task<int> Main(string[] args)
     {

         Server server = new Server();
         await server.Start();
         
        return 1;

    }
}