using System.Net;
using AccountingService.Dto;
using AccountingService.Dto.Requests;
using AccountingService.Dto.Responses;
using AccountingService.Exceptions;
using AccountingService.Models;
using AccountingService.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountingService.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientRegistrationService, IMapper mapper)
        {
            _clientService = clientRegistrationService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientDto>), (int)HttpStatusCode.OK)]
        public IActionResult List()
        {
            IEnumerable<ClientDto> result = _clientService
                .ListClients()
                .Select(_mapper.Map<ClientDto>);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Client client = await _clientService.GetClient(id);
                var result = _mapper.Map<ClientDto>(client);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound(new NotFoundResponse { Id = id });
            }
        }

        [HttpPost(nameof(Register))]
        [ProducesResponseType(typeof(ClientDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Register([FromBody] RegisterClientRequest request)
        {
            var client = _mapper.Map<Client>(request);
            Client created = await _clientService.RegisterClient(client);
            var result = _mapper.Map<ClientDto>(created);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}