using Microsoft.AspNetCore.Mvc;
using Parfete.Api.Services;

namespace Parfete.Api.Controllers
{
    [ApiVersion("1.0")]
     [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly ILogger<PartiesController> _logger;
        private readonly IGetParties _partiesService;

        public PartiesController(ILogger<PartiesController> logger, IGetParties partiesService)
        {
            _logger = logger;
            _partiesService = partiesService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult Get()
        {
            return Ok(_partiesService.GetAll());
        }
    }
}