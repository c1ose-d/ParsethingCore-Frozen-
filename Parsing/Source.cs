namespace Parsing;

public class Source : Procurement
{
    public Source(string requestUri)
    {
        RequestUri = requestUri;
        Request();
        if (Input != string.Empty)
        {
            Number = new GetNumber().Result;
            Law = new() { Number = new GetLawNumber().Result };
            Method = new() { Text = new GetMethodText().Result };
            Platform = new() { Name = new GetPlatformName().Result, Address = new GetPlatformAddress().Result };
            Organization = new() { Name = new GetOrganizationName().Result, PostalAddress = new GetOrganizationPostalAddress().Result };
            Object = new GetObject().Result;
            Location = new GetLocation().Result;
            try
            {
                StartDate = Convert.ToDateTime(new GetStartDate().Result);
            }
            catch { }
            try
            {
                Deadline = Convert.ToDateTime(new GetDeadline().Result);
            }
            catch { }
            TimeZone = new() { Offset = new GetTimeZoneOffset().Result };
            try
            {
                InitialPrice = Convert.ToDecimal(new GetInitialPrice().Result);
            }
            catch { }
            Securing = new GetSecuring().Result;
            Enforcement = new GetEnforcement().Result;
            Warranty = new GetWarranty().Result;
        }
    }

    private void Request()
    {
        GetRequest request = new(RequestUri);
        request.Returns();
        Input = request.Input;
    }

    public static string Input { get; set; } = null!;

    private class GetNumber : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@">№ (?<val>.*?)<", RegexOptions) };
        public GetNumber() : base(Input) { }
    }

    private class GetLawNumber : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"<div class=""cardMainInfo__title d-flex text-truncate""\n(?<space>.*?)\n(?<space>.*?)>(?<val>.*?)\n(?<space>.*?)</div>", RegexOptions), new(@"<div class=""registry-entry__header-top__title"">\n *(?<val>.*?) ", RegexOptions) };
        public GetLawNumber() : base(Input) { }
    }

    private class GetMethodText : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Способ определения поставщика \(подрядчика, исполнителя\)</(?<space>.*?)>\n *<(?<space>.*?)>(?<val>.*?)</(?<space>.*?)>\n", RegexOptions), new(@"Способ(?<space>.*?)\n(?<space>.*?)info"">(?<val>.*?)<", RegexOptions), new(@"Способ(?<space>.*?)\n(?<space>.*?)\n *(?<val>.*?)\n", RegexOptions) };
        public GetMethodText() : base(Input) { }
    }

    private class GetPlatformName : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Наименование электронной площадки(?<space>.*?)>\n(?<space>.*?)info"">(?<val>.*?)</", RegexOptions), new(@"Наименование электронной площадки(?<space>.*?)>\n(?<space>.*?)>\n *(?<val>.*?)\n", RegexOptions) };
        public GetPlatformName() : base(Input) { }
    }

    private class GetPlatformAddress : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Адрес электронной площадки(?<space>.*?)</span>(?<space>.*?)href=""(?<val>.*?)""", RegexOptions), new(@"Адрес электронной площадки(?<space>.*?)>\n(?<space>.*?)>\n(?<space>.*?)href=""(?<val>.*?)""", RegexOptions) };
        public GetPlatformAddress() : base(Input) { }
    }

    private class GetOrganizationName : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"<a href=""(?<space>.*?)epz/organization/view(?<space>.*?)>\s*(?<val>.*?)\s*<", RegexOptions), new(@"<a href=""(?<space>.*?)epz/organization/view(?<space>.*?)>(?<val>.*?)<", RegexOptions), new(@"padding"">(?<space>.*?)<a href=""(?<space>.*?)epz/organization/view(?<space>.*?)>(?<val>.*?)</a>", RegexOptions) };
        public GetOrganizationName() : base(Input) { }
    }

    private class GetOrganizationPostalAddress : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Почтовый адрес(?<space>.*?)>\n(?<space>.*?)\n *(?<val>.*?)\n", RegexOptions) };
        public GetOrganizationPostalAddress() : base(Input) { }
    }

    private class GetObject : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Объект закупки</span>\n(?<space>.*?)>(?<val>.*?)<", RegexOptions), new(@"Объект закупки</div>\n(?<space>.*?)\n *(?<val>.*?)\n", RegexOptions) };
        public GetObject() : base(Input) { }
    }

    private class GetLocation : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Место нахождения(?<space>.*?)>\n(?<space>.*?)\n *(?<val>.*?)\n", RegexOptions) };
        public GetLocation() : base(Input) { }
    }

    private class GetStartDate : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"начала(?<space>.*?)>\n(?<space>.*?)\n *(?<val>..\...\..... ..:..)", RegexOptions) };
        public GetStartDate() : base(Input) { }
    }

    private class GetDeadline : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"окончания(?<space>.*?)>\n(?<space>.*?)\n *(?<val>..\...\..... ..:..)", RegexOptions) };
        public GetDeadline() : base(Input) { }
    }

    private class GetTimeZoneOffset : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@" (?<val>МСК.*?) ", RegexOptions) };
        public GetTimeZoneOffset() : base(Input) { }
    }

    private class GetInitialPrice : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Максимальное значение цены контракта\n *</(?<space>.*?)>\n *<(?<space>.*?)>\n *(?<val>.*?)\n", RegexOptions), new(@"Начальная \(максимальная\) цена контракта\n *</(?<space>.*?)>\n *<(?<space>.*?)>\n *(?<val>.*?)\n", RegexOptions) };
        public GetInitialPrice() : base(Input) { }
    }

    private class GetSecuring : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Размер обеспечения заявки</(?<space>.*?)>\n *<(?<space>.*?)>\n *(?<val>.*?)\n", RegexOptions) };
        public GetSecuring() : base(Input) { }
    }

    private class GetEnforcement : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Размер обеспечения исполнения контракта</(?<space>.*?)>\s*<(?<space>.*?)>\n(?<val>.*?)</(?<space>.*?)>", RegexOptions) };
        public GetEnforcement() : base(Input) { }
    }

    private class GetWarranty : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Размер обеспечения гарантийных обязательств</(?<space>.*?)>\n *<(?<space>.*?)>\n *(?<val>.*?)\n", RegexOptions) };
        public GetWarranty() : base(Input) { }
    }

    public override string ToString()
    {
        return $"{RequestUri}\n{Number}\n{Object}\n{Location}\n{TimeZone.Offset}\n{InitialPrice}";
    }
}