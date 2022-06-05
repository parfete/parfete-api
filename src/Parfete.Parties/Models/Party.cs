namespace Parfete.Parties.Models
{
    public record Party
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = "";
        public DateTime Date { get; init; }
        public string Address { get; init; } = "";
    }
}