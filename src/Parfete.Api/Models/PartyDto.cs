namespace Parfete.Api.Models
{
    public record PartyDto : Dto
    {
        public PartyDto(params Link[] links)
        {
            foreach (var link in links)
            {
                Add(link);
            }
        }

        public string Name { get; init; } = "";
        public string Address { get; init; } = "";
        public DateTime? Date { get; init; }
    }
}