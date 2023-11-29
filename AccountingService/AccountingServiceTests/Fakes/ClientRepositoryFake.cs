using AccountingService.Data.Repositories;
using AccountingService.Models;

namespace AccountingServiceTests.Fakes
{
    public class ClientRepositoryFake : IClientRepository
    {
        private readonly Dictionary<int, Client> _clients = new();

        public IQueryable<Client> List()
        {
            return _clients.Values.AsQueryable();
        }

        public Task<Client?> Read(int id)
        {
            Client? result = _clients.ContainsKey(id) 
                ? _clients[id] 
                : null;

            return Task.FromResult(result);
        }

        public Task<Client?> ReadWithWallet(int id)
        {
            return Read(id);
        }

        public Task<Client> Create(Client client)
        {
            _clients.Add(client.Id, client);

            return Task.FromResult(client);
        }

        public Task<Client> Update(int id, Client client)
        {
            _clients[id] = client;

            return Task.FromResult(client);
        }
    }
}