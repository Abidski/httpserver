
namespace httpserver;

public class HttpResponse
{
    private int StatusCode;
    private string Version;
    private string Phrase;
    private string Body;
    private Dictionary<string, string> headers = new Dictionary<string, string>();

    public HttpResponse(int code, string version, string phrase, string body)
    {
        this.StatusCode = code;
        this.Version = version;
        this.Phrase = phrase;
        this.Body = body;
    }

    public override string ToString()
    {

        return $"{Version} {StatusCode} {Phrase}\n\n{Body}";

    }

}
