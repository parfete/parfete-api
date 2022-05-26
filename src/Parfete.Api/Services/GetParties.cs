using Parfete.Api.Models;
using Bogus;

namespace Parfete.Api.Services
{
    public class GetParties : IGetParties
    {
        private readonly List<Party> _parties = new List<Party>();

        public GetParties()
        {
            _parties.AddRange(SeedParties());
        }

        private IEnumerable<Party> SeedParties()
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var partiesCount = rnd.Next(1, 20);

            for (int i = 0; i < partiesCount; i++)
            {
                yield return SeedParty();
            }
        }

        private Party SeedParty()
        {
            var faker = new Faker("fr");
            return new Party{
                Name = faker.Person.FullName,
                Address = faker.Person.Address.City,
                Date = faker.Date.SoonDateOnly().ToDateTime(faker.Date.SoonTimeOnly())
            };
        }

        public IReadOnlyCollection<Party> GetAll()
        {
            return _parties;
        }
    }
}