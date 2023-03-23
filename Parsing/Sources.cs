namespace Parsing;

public class Sources : List<Source>
{
    public Sources()
    {
        RequestUris requestUris = new();
        foreach (string requestUri in requestUris)
        {
            Source source = new(requestUri);
            if (source.Number != null)
            {
                Add(source);
            }
        }
    }

    private class RequestUris : List<string>
    {
        public RequestUris()
        {
            for (int i = 1; i <= 100; i++)
            {
                RequestUri = RequetUriRegex.Replace(RequestUri, $"pageNumber={i}");
                Request();
                foreach (string result in new FragmentUri(Input).Results)
                {
                    Add($"{BaseUri}{result}");
                }
            }
        }

        private void Request()
        {
            GetRequest request = new(RequestUri);
            Input = request.Input;
        }

        private string BaseUri { get; } = "https://zakupki.gov.ru/epz/order/notice/";
        private string RequestUri { get; } = "https://zakupki.gov.ru/epz/order/extendedsearch/results.html?searchString=%D0%BA%D0%BE%D0%BC%D0%BF%D1%8C&morphology=on&search-filter=%D0%94%D0%B0%D1%82%D0%B5+%D1%80%D0%B0%D0%B7%D0%BC%D0%B5%D1%89%D0%B5%D0%BD%D0%B8%D1%8F&pageNumber=1&sortDirection=false&recordsPerPage=_10&showLotsInfoHidden=false&sortBy=UPDATE_DATE&fz44=on&fz223=on&af=on&ca=on&pc=on&pa=on&currencyIdGeneral=-1";
        private Regex RequetUriRegex { get; } = new(@"pageNumber=\d+", RegexOptions);
        private class FragmentUri
        {
            private Regex Regex { get; } = new(@"<a target=""_blank"" href=""/epz/order/notice/(?<val>.*?)"">(?<space>.*?)</a>\n", RegexOptions);
            private MatchCollection MatchCollection { get; set; } = null!;
            public List<string> Results { get; set; } = new();
            public FragmentUri(string input)
            {
                try
                {
                    MatchCollection = Regex.Matches(input);
                    foreach (Match match in MatchCollection.Cast<Match>())
                    {
                        Results.Add(Regex.Split(match.Value)[^3]);
                    }
                }
                catch { }
            }
        }
    }
    public static string Input { get; set; } = null!;
    private static RegexOptions RegexOptions { get; } = RegexOptions.Compiled | RegexOptions.Singleline;
}