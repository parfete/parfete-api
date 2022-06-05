using Parfete.Api.Models;
using Parfete.Api.Tests.Shared;
using Parfete.Parties;
using Parfete.Parties.Service.Models;
using TechTalk.SpecFlow.Assist;
namespace Parfete.Api.Tests.Parties.Steps
{
    [Binding]
    public class GetPartiesSteps : GetPartiesContext, IClassFixture<Server>
    {
        private readonly HttpClient _client;

        private readonly Mock<IPartiesRepository> _partiesRepository = new Mock<IPartiesRepository>();

        public GetPartiesSteps(ScenarioContext scenarioContext,Server server):base(scenarioContext)
        {
            _client = server.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IPartiesRepository>(_ => _partiesRepository.Object);
                });
            }).CreateClient();
        }

        [Given(@"I use the API version (.*)")]
        public void GivenIusetheAPIversion(int apiVersion)
        {
            ApiVersion = apiVersion;
        }

        [Given(@"I have a list of parties")]
        public void GivenIHaveAListOfParties(Table table)
        {
            Parties = table.CreateSet<PartyEntity>(row => new PartyEntity
            {
                Id = Guid.NewGuid(),
                Name = row["Name"],
                Address = row["Address"],
                Date = DateTime.Parse(row["Date"])
            });

            _partiesRepository.Setup(p => p.GetAllParties()).Returns(new List<PartyEntity>(Parties));
        }

        [When(@"I retrieve the list of parties")]
        public void WhenIretrievethelistofparties()
        {
            _client.GetAsync(GetAllPartiesRoute(ApiVersion));
        }

        [Then(@"I should get a list of parties")]
        public void ThenIshouldgetalistofparties(Table table)
        {
            var expected = table.CreateSet<PartyDto>(row => new PartyDto
            {
                Name = row["Name"],
                Address = row["Address"],
                Date = DateTime.Parse(row["Date"])
            });
        }

        private static string GetAllPartiesRoute(int apiVersion)
            => $"api/v{apiVersion}/parties";
    }

    public class GetPartiesContext
    {
        protected readonly ScenarioContext _scenarioContext;
        protected GetPartiesContext(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public int ApiVersion
        {
            get => _scenarioContext.Get<int>(nameof(ApiVersion));
            set => _scenarioContext.Add(nameof(ApiVersion), value);
        }

        public IEnumerable<PartyEntity> Parties
        {
            get => _scenarioContext.Get<IEnumerable<PartyEntity>>(nameof(Parties));
            set => _scenarioContext.Add(nameof(Parties), value);
        }
    }
}