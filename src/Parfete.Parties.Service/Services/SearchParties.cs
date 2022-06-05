using CSharpFunctionalExtensions;
using Parfete.Parties.Models;

namespace Parfete.Parties.Service
{
    public class SearchParties : ISearchParties
    {
        private readonly IPartiesRepository _partiesRepository;

        public SearchParties(IPartiesRepository partiesRepository)
        {
            _partiesRepository = partiesRepository;
        }

        public IReadOnlyCollection<Party> GetAll()
            => _partiesRepository.GetAllParties().Select(p => new Party
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Date = p.Date
            }).ToList();

        public Maybe<Party> GetById(Guid id)
        {
            var party = _partiesRepository.FindPartyById(id);

            if (party == null)
            {
                return Maybe.None;
            }

            return new Party
            {
                Id = party.Id,
                Name = party.Name,
                Address = party.Address,
                Date = party.Date
            };
        }

    }
}