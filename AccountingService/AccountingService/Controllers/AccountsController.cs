using System.Net;
using AccountingService.Dto.Requests;
using AccountingService.Dto.Responses;
using AccountingService.Exceptions;
using AccountingService.Filters;
using AccountingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [TypeFilter(typeof(UnhandledExceptionFilter))]
    public class AccountsController : ControllerBase
    {
        private readonly IClientAccountingService _accountingService;

        public AccountsController(IClientAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        [HttpGet("[action]/{clientId}")]
        [ProducesResponseType(typeof(AccountAmountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAccountAmount(int clientId)
        {
            try
            {
                decimal amount = await _accountingService.GetAccountAmount(clientId);

                return Ok(new AccountAmountResponse { Amount = amount });
            }
            catch (NotFoundException)
            {
                return NotFound(new NotFoundResponse { Id = clientId });
            }
        }

        [HttpPost(nameof(CreditAccount))]
        [ProducesResponseType(typeof(AccountAmountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreditAccount([FromBody] CreditAccountRequest request)
        {
            try
            {
                decimal newAmount = await _accountingService.CreditClientAccount(request.ClientId, request.Amount);

                return Ok(new AccountAmountResponse { Amount = newAmount });
            }
            catch (NotFoundException)
            {
                return NotFound(new NotFoundResponse { Id = request.ClientId });
            }
        }

        [HttpPost(nameof(DebitAccount))]
        [ProducesResponseType(typeof(AccountAmountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.MethodNotAllowed)]
        public async Task<IActionResult> DebitAccount([FromBody] DebitAccountRequest request)
        {
            try
            {
                decimal newAmount = await _accountingService.DebitClientAccount(request.ClientId, request.Amount);

                return Ok(new AccountAmountResponse { Amount = newAmount });
            }
            catch (NotFoundException)
            {
                return NotFound(new NotFoundResponse { Id = request.ClientId });
            }
            catch (InvalidOperationException)
            {
                return MethodNotAllowed("Tried to withdraw more than the current amount");
            }
        }

        private ObjectResult MethodNotAllowed(object? value)
        {
            return StatusCode(405, value);
        }
    }
}