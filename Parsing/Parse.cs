namespace Parsing;

public class Parse
{
    public Parse(string input)
    {
        foreach (Regex regex in Regexes)
        {
            try
            {
                Result = regex.Split(input)[^2];
                foreach (KeyValuePair<string, string> replacement in Replacements)
                {
                    Result = Result.Replace(replacement.Key, replacement.Value);
                }
                if (Result != null)
                {
                    RemoveWhitespace();
                    break;
                }
            }
            catch { }
        }
    }

    public static RegexOptions RegexOptions { get; } = RegexOptions.Compiled | RegexOptions.Singleline;
    public virtual List<Regex> Regexes { get; } = null!;
    private Dictionary<string, string> Replacements { get; } = new() { { "«", "\"" }, { "»", "\"" }, { "&nbsp;", " " }, { "&#8381;", "Российский рубль" }, { "&#034;", "\"" }, { "\n", "" }, { "&ndash;", "—" }, { "&laquo;", "\"" }, { "&raquo;", "\"" }, { "&quot;", "\"" }, { "&mdash;", "—" }, { "( ", "(" }, { " )", ")" } };
    private void RemoveWhitespace()
    {
        if (Result != null)
        {
            Regex regex = new(@"(\s\s)+");
            Regex whitespaceBetween = new(@"\S(\s\s)+\S");
            Regex whitespacePreview = new(@"^\s+");
            if (whitespaceBetween.IsMatch(Result))
            {
                Result = regex.Replace(Result, " ");
            }
            Result = regex.Replace(Result, "");
            Result = whitespacePreview.Replace(Result, "");
        }
    }

    public string Result { get; set; } = string.Empty;
}