namespace Database;

public partial class Platform
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();

    public override bool Equals(object? obj)
    {
        return obj is Platform platform && Name == platform.Name && Address == platform.Address;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Address);
    }
}