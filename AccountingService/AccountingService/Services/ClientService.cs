using AccountingService.Data.Repositories;
using AccountingService.Exceptions;
using AccountingService.Models;

namespace AccountingService.Services
{
    public class ClientService : IClientService
    {
        private const string ClientNotFoundMessage = "Specified client was not found";

        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IQueryable<Client> ListClients()
        {
            return _clientRepository.List();
        }

        public async Task<Client> GetClient(int clientId)
        {
            Client? client = await _clientRepository.Read(clientId);
            if (client == null)
            {
                throw new NotFoundException(ClientNotFoundMessage);
            }

            return client;
        }

        public async Task<Client> RegisterClient(Client client)
        {
            client = client.WithWallet(new Wallet(0));

            return await _clientRepository.Create(client);
        }
    }
}