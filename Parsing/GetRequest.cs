using System.Diagnostics;

namespace Parsing;

public class GetRequest
{
    public GetRequest(string requestUri)
    {
        RequestUri = requestUri;
        HttpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        try
        {
            HttpClient.Send(new HttpRequestMessage(HttpMethod.Patch, RequestUri));
            Input = HttpClient.GetStringAsync(RequestUri).Result;
            Trace.WriteLine($"{DateTime.Now}\n{RequestUri}\nIs complited successfully.\n");
        }
        catch (Exception e)
        {
            Trace.WriteLine($"{DateTime.Now}\n{RequestUri}\n{e.InnerException?.Message}\n");
        }
        HttpClient.Dispose();
    }

    private string RequestUri { get; set; } = null!;
    private HttpClient HttpClient { get; set; }
    public string Input { get; set; } = string.Empty;
}