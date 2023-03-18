namespace Database;

public partial class Method
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();

    public override bool Equals(object? obj)
    {
        return obj is Method method && Text == method.Text;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Text);
    }
}