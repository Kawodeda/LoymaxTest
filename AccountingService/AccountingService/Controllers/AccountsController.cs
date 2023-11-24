using System.Net;
using AccountingService.Data.Repositories;
using AccountingService.Dto.Requests;
using AccountingService.Dto.Responses;
using AccountingService.Exceptions;
using AccountingService.Models;
using AccountingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientAccountingService _accountingService;

        public AccountsController(IClientRepository clientRepository, IClientAccountingService accountingService)
        {
            _clientRepository = clientRepository;
            _accountingService = accountingService;
        }

        [HttpGet("[action]/{clientId}")]
        [ProducesResponseType(typeof(AccountAmountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAccountAmount(int clientId)
        {
            Client? client = await _clientRepository.ReadWithWallet(clientId);
            if (client == null || client.Wallet == null)
            {
                return NotFound(new NotFoundResponse { Id = clientId });
            }

            return Ok(new AccountAmountResponse { Amount = client.Wallet.Amount });
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