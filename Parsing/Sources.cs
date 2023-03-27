namespace Parsing;

public class Sources : List<Source>
{
    public Sources()
    {
        GetSources sources = new();
        foreach (Source source in sources)
        {
            if (!source.IsSkippable)
            {
                source.GetInnerObjects();
                Add(source);
            }
            else
            {
                Trace.WriteLine($"{DateTime.Now}\n{source.Number}\nIs skiped.\n");
            }
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
                foreach (Match procurementCard in procurementCards.Cast<Match>())
                {
                    Add(new(procurementCard.Value));
                }
            }
        }

        private string? Input { get; set; }
        private static RegexOptions RegexOptions { get; } = RegexOptions.Compiled | RegexOptions.Singleline;
        private Regex Regex { get; set; } = new(@" *<div class=""search-registry-entry-block box-shadow-search-input"">(?<val>.*?)\n        </div>", RegexOptions);
    }
}