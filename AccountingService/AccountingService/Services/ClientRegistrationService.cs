using AccountingService.Data.Repositories;
using AccountingService.Models;

namespace AccountingService.Services
{
    public class ClientRegistrationService : IClientRegistrationService
    {
        private readonly IClientRepository _clientRepository;

        public ClientRegistrationService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> RegisterClient(Client client)
        {
            client = client.WithWallet(new Wallet(0));

            return await _clientRepository.Create(client);
        }
    }
}