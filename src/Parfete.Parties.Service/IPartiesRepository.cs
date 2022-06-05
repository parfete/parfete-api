using Bogus;
using Parfete.Parties.Service.Models;

namespace Parfete.Parties
{
    public interface IPartiesRepository
    {
        IReadOnlyCollection<PartyEntity> GetAllParties();
        PartyEntity? FindPartyById(Guid id);
    }

    internal class PartiesRepository : IPartiesRepository
    {
        private static readonly List<PartyEntity> _parties = new List<PartyEntity>();

        public PartiesRepository()
        {
            if (_parties.Count > 0)
            {
                return;
            }
            _parties.AddRange(SeedParties());
        }

        public IReadOnlyCollection<PartyEntity> GetAllParties() => _parties;

        public PartyEntity? FindPartyById(Guid id) => _parties.Find(p=>p.Id == id);

        private IEnumerable<PartyEntity> SeedParties()
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var partiesCount = rnd.Next(1, 20);

            for (int i = 0; i < partiesCount; i++)
            {
                yield return SeedParty();
            }
        }

        private PartyEntity SeedParty()
        {
            var faker = new Faker("fr");
            return new PartyEntity{
                Id = Guid.NewGuid(),
                Name = faker.Person.FullName,
                Address = faker.Person.Address.City,
                Date = faker.Date.SoonDateOnly().ToDateTime(faker.Date.SoonTimeOnly())
            };
        }
    }
}