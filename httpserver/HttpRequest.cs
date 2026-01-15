namespace httpserver;

public class HttpRequest
{
    private string Method;
    private string Uri;
    private string Version;
    private Dictionary<String, String> Headers = new Dictionary<String, String>();
    enum METHOD
    {
        Get,
    }

    public HttpRequest(string method, string uri, string version)
    {
        this.Method = method;
        this.Uri = uri;
        this.Version = version;
    }

    public void AddHeaders(string[] headers)
    {
        
    }
}
