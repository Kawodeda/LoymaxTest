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

        public Wallet WithClientId(int clientId)
        {
            return new Wallet(Id, Amount, clientId);
        }

        public Wallet CreditAmount(decimal amount)
        {
            return new Wallet(Id, Amount + amount, ClientId);
        }

        public Wallet DebitAmount(decimal amount)
        {
            return new Wallet(Id, Amount - amount, ClientId);
        }
    }
}