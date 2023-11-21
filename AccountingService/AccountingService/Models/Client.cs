namespace AccountingService.Models
{
    public class Client
    {
        public Client(string lastName, string firstName, string middleName, DateOnly birthDate) 
            : this(0, lastName, firstName, middleName, birthDate)
        {

        }

        private Client(int id, string lastName, string firstName, string middleName, DateOnly birthDate)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
        }

        public int Id { get; }

        public string LastName { get; }

        public string FirstName { get; }

        public string MiddleName { get; }

        public DateOnly BirthDate { get; }

        public Wallet? Wallet { get; }
    }
}