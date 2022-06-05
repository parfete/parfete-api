using CSharpFunctionalExtensions;
using Parfete.Parties.Models;

namespace Parfete.Parties
{
    public class GetParties : IGetParties
    {
        private readonly ISearchParties _searchParties;

        public GetParties(ISearchParties searchParties)
        {
            _searchParties = searchParties;
        }

        public IReadOnlyCollection<Party> GetAll() => _searchParties.GetAll();

        public Maybe<Party> GetById(Guid id) => _searchParties.GetById(id);
    }
}