using System.Net;
using AccountingService.Data.Repositories;
using AccountingService.Dto;
using AccountingService.Dto.Requests;
using AccountingService.Dto.Responses;
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
        private readonly IClientRepository _clientRepository;
        private readonly IClientRegistrationService _clientRegistrationService;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository clientRepository, IClientRegistrationService clientRegistrationService, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _clientRegistrationService = clientRegistrationService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientDto>), (int)HttpStatusCode.OK)]
        public IActionResult List()
        {
            IEnumerable<ClientDto> result = _clientRepository
                .List()
                .Select(_mapper.Map<ClientDto>);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            Client? client = await _clientRepository.Read(id);
            if (client == null)
            {
                return NotFound(new NotFoundResponse { Id = id });
            }

            var result = _mapper.Map<ClientDto>(client);

            return Ok(result);
        }

        [HttpPost(nameof(Register))]
        [ProducesResponseType(typeof(ClientDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Register([FromBody] RegisterClientRequest request)
        {
            var client = _mapper.Map<Client>(request);
            Client created = await _clientRegistrationService.RegisterClient(client);
            var result = _mapper.Map<ClientDto>(created);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}