namespace Database;

public partial class TimeZone
{
    public int Id { get; set; }

    public string Offset { get; set; } = null!;

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();

    public override bool Equals(object? obj)
    {
        return obj is TimeZone zone && Offset == zone.Offset;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Offset);
    }
}