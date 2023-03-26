namespace Database.Entities;

public partial class SourceStatе
{
    public int Id { get; set; }

    public string Kind { get; set; } = null!;

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();
}