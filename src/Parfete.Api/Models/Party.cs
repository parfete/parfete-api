namespace Parfete.Api.Models
{
    public record Party
    {
        public string Name { get; init; } = "";
        public DateTime Date { get; init; }
        public string Address { get; init; } = "";
    }
}