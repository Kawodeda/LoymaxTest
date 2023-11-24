using AccountingService.Data.Repositories;
using AccountingService.Exceptions;
using AccountingService.Models;

namespace AccountingService.Services
{
    public class ClientAccountingService : IClientAccountingService
    {
        private readonly IClientRepository _clientRepository;

        public ClientAccountingService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<decimal> CreditClientAccount(int clientId, decimal amount)
        {
            Client? client = await _clientRepository.ReadWithWallet(clientId);
            if (client?.Wallet == null)
            {
                throw new NotFoundException();
            }

            client = client.WithWallet(client.Wallet.CreditAmount(amount));
            client = await _clientRepository.Update(client.Id, client);

            return client.Wallet!.Amount;
        }

        public async Task<decimal> DebitClientAccount(int clientId, decimal amount)
        {
            Client? client = await _clientRepository.ReadWithWallet(clientId);
            if (client?.Wallet == null)
            {
                throw new NotFoundException();
            }

            client = client.WithWallet(client.Wallet.DebitAmount(amount));
            client = await _clientRepository.Update(client.Id, client);

            return client.Wallet!.Amount;
        }
    }
}