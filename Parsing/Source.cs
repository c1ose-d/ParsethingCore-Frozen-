using Database.Entities;

namespace Parsing;

public class Source : Procurement
{
    public Source(string procurementCard)
    {
        ProcurementCard = procurementCard;
        Initialize();
    }

    public static string ProcurementCard { get; set; } = null!;
    public static string Input { get; set; } = null!;

    private void Initialize()
    {
        RequestUri = $"https://zakupki.gov.ru/epz/order/notice{new GetRequestUri().Result}";
        Number = new GetNumber().Result;
        ProcurementSourceStatе = new() { Kind = new GetProcurementSourceStateKind().Result };
        Law = new() { Number = new GetLawNumber().Result };
        Object = new GetObject().Result;
        InitialPrice = Convert.ToDecimal(new GetInitialPrice().Result);
        Organization = new() { Name = new GetOrganizationName().Result };
    }

    public void GetInnerObjects()
    {
        GetInput();
    }

    private void GetInput()
    {
        GetRequest request = new(RequestUri);
        Input = request.Input;
    }

    private class GetRequestUri : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"<a target=""_blank"" href=""/epz/order/notice(?<val>.*?)"">", RegexOptions) };
        public GetRequestUri() : base(ProcurementCard) { }
    }

    private class GetNumber : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"№ (?<val>.*?)\n", RegexOptions) };
        public GetNumber() : base(ProcurementCard) { }
    }

    private class GetProcurementSourceStateKind : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"<div class=""registry-entry__header-mid__title text-normal"">(?<val>.*?)</div>", RegexOptions) };
        public GetProcurementSourceStateKind() : base(ProcurementCard) { }
    }

    private class GetLawNumber : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"<div class=""col-9 p-0 registry-entry__header-top__title text-truncate""(?<space>.*?)>\n *(?<val>.*?)\n", RegexOptions) };
        public GetLawNumber() : base(ProcurementCard) { }
    }

    private class GetObject : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Объект закупки</(?<space>.*?)>\n(?<space>.*?)>(?<val>.*?)<", RegexOptions) };
        public GetObject() : base(ProcurementCard) { }
    }

    private class GetInitialPrice : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Начальная цена(?<space>.*?)value"">(?<val>.*,..?) ", RegexOptions) };
        public GetInitialPrice() : base(ProcurementCard) { }
    }

    private class GetOrganizationName : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"href=""/epz/organization/view/(?<space>.*?)>\n *(?<val>.*?)\n", RegexOptions) };
        public GetOrganizationName() : base(ProcurementCard) { }
    }

    private class GetMethodText : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"<div class=""col-9 p-0 registry-entry__header-top__title text-truncate""(?<space>.*?)>\n *(?<space>.*?)\n *(?<val>.*?)\n", RegexOptions) };
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

    private class GetOrganizationPostalAddress : Parse
    {
        public override List<Regex> Regexes { get; } = new() { new(@"Почтовый адрес(?<space>.*?)>\n(?<space>.*?)\n *(?<val>.*?)\n", RegexOptions) };
        public GetOrganizationPostalAddress() : base(Input) { }
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
        return $"{RequestUri}\n{Number}\n{Object}\n{Location}\n{TimeZone?.Offset}\n{InitialPrice}";
    }
}