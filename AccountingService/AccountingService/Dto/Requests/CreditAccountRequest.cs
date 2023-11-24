using System.ComponentModel.DataAnnotations;

namespace AccountingService.Dto.Requests
{
    public class CreditAccountRequest
    {
        [Required]
        public required int ClientId { get; set; }

        [Required]
        public required decimal Amount { get; set; }
    }
}