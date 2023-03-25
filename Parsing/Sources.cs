namespace Parsing;

public class Sources : List<Source>
{
    public Sources()
    {
        GetSources sources = new();
        foreach (Source source in sources)
        {
            Add(source);
        }
    }

    private class GetSources : List<Source>
    {
        public GetSources()
        {
            GetRequest request = new(Resources.RequestUri);
            Input = request.Input;

            if (Input != null)
            {
                MatchCollection procurementCards = Regex.Matches(Input);
                foreach (Match procurementCard in procurementCards)
                {
                    Add(new(procurementCard.Value));
                }
            }
        }

        private string? Input { get; set; }
        private static RegexOptions RegexOptions { get; } = RegexOptions.Compiled | RegexOptions.Singleline;
        private Regex Regex { get; set; } = new(@" *<div class=""search-registry-entry-block box-shadow-search-input"">(?<val>.*?)\n        </div>", RegexOptions);
    }

    public static string Input { get; set; } = null!;
}