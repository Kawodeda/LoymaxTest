using AccountingService.Models;

namespace AccountingService.Services
{
    public interface IClientService
    {
        IQueryable<Client> ListClients();

        Task<Client> GetClient(int clientId);

        Task<Client> RegisterClient(Client client);
    }
}