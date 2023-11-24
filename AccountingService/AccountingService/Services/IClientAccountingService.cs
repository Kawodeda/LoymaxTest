using AccountingService.Models;

namespace AccountingService.Services
{
    public interface IClientAccountingService
    {
        Task<decimal> GetAccountAmount(int clientId);

        Task<decimal> CreditClientAccount(int clientId, decimal amount);

        Task<decimal> DebitClientAccount(int clientId, decimal amount);
    }
}