namespace AccountingService.Models
{
    public class Wallet
    {
        public Wallet(int id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }

        public int Id { get; }

        public decimal Amount { get; }

        public Client? Client { get; }
    }
}