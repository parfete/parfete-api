using CSharpFunctionalExtensions;
using Parfete.Parties.Models;

namespace Parfete.Parties
{
    public interface ISearchParties
    {
        IReadOnlyCollection<Party> GetAll();
        Maybe<Party> GetById(Guid id);
    }
}