namespace AccountingService.Models
{
    public class Client
    {
        public Client(int id, string lastName, string firstName, string middleName, DateOnly birthDate)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
        }

        public int Id { get; }

        public string LastName { get; set; }

        public string FirstName { get; }

        public string MiddleName { get; }

        public DateOnly BirthDate { get; }
    }
}