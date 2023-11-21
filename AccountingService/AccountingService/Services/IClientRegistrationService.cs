using AccountingService.Models;

namespace AccountingService.Services
{
    public interface IClientRegistrationService
    {
        Task<Client> RegisterClient(Client client);
    }
}