namespace Database;

public partial class Law
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();

    public override bool Equals(object? obj)
    {
        return obj is Law law && Number == law.Number;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Number);
    }
}