using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Parfete.Api.Models;

namespace Parfete.Api.Controllers
{
    [ApiController]
    public abstract class HateoasController : ControllerBase
    {
        private readonly ILogger<PartiesController> _logger;
        
        private readonly IReadOnlyCollection<ActionDescriptor> _routes;
        
        protected HateoasController(ILogger<PartiesController> logger, IActionDescriptorCollectionProvider provider)
            => (_logger, _routes) = (logger, provider.ActionDescriptors.Items);
    
        protected Link UrlLink(string relation, string routeName, object? values = null)
        {
            var route = Route(routeName);

            if (route is null)
            {
                return new Link();
            }

            var method = Method(route);

            if (method is null)
            {
                return new Link();
            }

            var url = Url.Link(routeName, values);

            if (url is null)
            {
                return new Link();
            }

            return new Link
            {
                Href = url,
                Rel = relation,
                Type = method
            };
        }

        private ActionDescriptor? Route(string routeName) =>
            _routes.FirstOrDefault(_ => _.AttributeRouteInfo?.Name?.Equals(routeName) ?? false);

        private string? Method(ActionDescriptor route) =>
            route.ActionConstraints?
                .OfType<HttpMethodActionConstraint>()
                .FirstOrDefault()?
                .HttpMethods
                .FirstOrDefault();
    }
}