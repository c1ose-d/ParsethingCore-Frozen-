namespace Parsing;

public class GetRequest
{
    public GetRequest(string requestUri)
    {
        RequestUri = requestUri;
    }

    public string RequestUri { get; set; } = null!;
    private byte[]? Bytes { get; set; }
    public string Input { get; set; } = string.Empty;
    public void Returns()
    {
        using HttpClient httpClient = new();
        try
        {
            Bytes = httpClient.GetByteArrayAsync(RequestUri).Result;
            Console.WriteLine($"GET Request from {RequestUri} was successfully");
        }
        catch
        {
            Console.WriteLine($"GET Request from {RequestUri} was unsuccessfully");
        }
        if (Bytes != null)
        {
            Input = Encoding.UTF8.GetString(Bytes);
        }
    }
}