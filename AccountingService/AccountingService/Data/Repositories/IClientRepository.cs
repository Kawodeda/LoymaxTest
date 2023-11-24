using AccountingService.Models;

namespace AccountingService.Data.Repositories
{
    public interface IClientRepository
    {
        IQueryable<Client> List();

        Task<Client?> Read(int id);

        Task<Client?> ReadWithWallet(int id);

        Task<Client> Create(Client client);

        Task<Client> Update(int id, Client client);
    }
}