using System.ComponentModel.DataAnnotations;

namespace AccountingService.Dto
{
    public class ClientDto
    {
        public required int Id { get; set; }

        public required string LastName { get; set; }

        public required string FirstName { get; set; }

        public required string MiddleName { get; set; }

        public required DateOnly BirthDate { get; set; }
    }
}