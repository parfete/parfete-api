namespace Parfete.Parties.Service.Models
{
    public record PartyEntity
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = "";
        public DateTime Date { get; init; }
        public string Address { get; init; } = "";
    }
}