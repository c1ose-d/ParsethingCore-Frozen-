namespace Database;

public partial class Procurement
{
    public int Id { get; set; }

    public string RequestUri { get; set; } = null!;

    public string Number { get; set; } = null!;

    public int? LawId { get; set; }

    public int? MethodId { get; set; }

    public int? PlatformId { get; set; }

    public int? OrganizationId { get; set; }

    public string Object { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? Deadline { get; set; }

    public int TimeZoneId { get; set; }

    public decimal InitialPrice { get; set; }

    public string? Securing { get; set; }

    public string? Enforcement { get; set; }

    public string? Warranty { get; set; }

    public virtual Law? Law { get; set; }

    public virtual Method? Method { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual Platform? Platform { get; set; }

    public virtual TimeZone? TimeZone { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Procurement procurement && Number == procurement.Number;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Number);
    }
}