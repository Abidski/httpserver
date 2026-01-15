using System.Diagnostics;

namespace httpserver;

public class HttpResponse
{
    private int StatusCode;
    private string Version;
    private string Phrase;
    private Dictionary<string, string> headers = new Dictionary<string, string>();
}