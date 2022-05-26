using Parfete.Api.Models;

namespace Parfete.Api.Services
{
    public interface IGetParties
    {
        IReadOnlyCollection<Party> GetAll();
    }
}