namespace AccountingService.Models
{
    public class Wallet
    {
        public Wallet(decimal amount) : this(0, amount, 0)
        {

        }

        private Wallet(int id, decimal amount, int clientId)
        {
            Id = id;
            Amount = amount;
            ClientId = clientId;
        }

        public int Id { get; }

        public decimal Amount { get; }

        public int ClientId { get; }
    }
}