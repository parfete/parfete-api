namespace Parfete.Api.Models
{
    public record Link
    {
        public Link() { }
        public Link(string href, string rel, string type)
            => (Href, Rel, Type) = (href, rel, type);

        public string Href { get; init; } = "";
        public string Type { get; init; } = "";
        public string Rel { get; init; } = "";
    }
}