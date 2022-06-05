using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Parfete.Api.Models;
using Parfete.Parties;

namespace Parfete.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PartiesController : HateoasController
    {
        private readonly ILogger<PartiesController> _logger;
        private readonly IGetParties _partiesService;

        public PartiesController(ILogger<PartiesController> logger, IGetParties partiesService, IActionDescriptorCollectionProvider provider) : base(logger, provider)
        {
            _logger = logger;
            _partiesService = partiesService;
        }

        [HttpGet(Name = nameof(GetParties))]
        [ProducesResponseType(typeof(PartiesDto), 200)]
        [MapToApiVersion("1.0")]
        public IActionResult GetParties()
        {
            Serilog.Log.Debug("test");
            _logger.LogDebug("Get Parties");

            var parties = _partiesService.GetAll().Select(p => new PartyDto(SelfParty(p.Id))
            {
                Name = p.Name,
                Address = p.Address,
                Date = p.Date
            });

            return Ok(new PartiesDto(parties, AllParties()));
        }

        [HttpGet("{id:guid}", Name = nameof(GetParty))]
        [MapToApiVersion("1.0")]
        public IActionResult GetParty(Guid id)
        {
            _logger.LogDebug("Get Party");

            var party = _partiesService.GetById(id);

            if (party.HasNoValue)
            {
                return NotFound();
            }

            return Ok(new PartyDto(SelfParty(party.Value.Id), AllParties())
            {
                Name = party.Value.Name,
                Address = party.Value.Address,
                Date = party.Value.Date
            });
        }

        private Link SelfParty(Guid id)
            => UrlLink("self", nameof(GetParty), new { id = id });

        private Link AllParties()
            => UrlLink("parties", nameof(GetParties));
    }
}