using System.ComponentModel.DataAnnotations;

namespace AccountingService.Dto.Requests
{
    public class RegisterClientRequest
    {
        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string MiddleName { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }
    }
}