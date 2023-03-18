namespace Database;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PostalAddress { get; set; } = null!;

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();

    public override bool Equals(object? obj)
    {
        return obj is Organization organization && Name == organization.Name && PostalAddress == organization.PostalAddress;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, PostalAddress);
    }
}