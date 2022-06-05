using System.Collections;

namespace Parfete.Api.Models
{
    public record PartiesDto : Dto
    {
        private readonly List<PartyDto> _parties = new List<PartyDto>();
        public PartiesDto(IEnumerable<PartyDto> parties, params Link[] links)
        {
            _parties.AddRange(parties);

            foreach (var link in links)
            {
                Add(link);
            }
        }

        public IReadOnlyCollection<PartyDto> Parties => _parties;
    }
}