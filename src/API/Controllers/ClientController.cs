using Application.DTOs.ClientDto;
using Application.DTOs.CommonDto;
using Application.UseCases.ClientUseCase;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientUseCase _clientUseCase;

        public ClientController(IClientUseCase clientUseCase)
        {
            _clientUseCase = clientUseCase;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> Create([FromBody] ClientCreateRequestDto request)
        {
            var response = await _clientUseCase.CreateAsync(request);
            return Ok(response);
        }

        [HttpGet("Detail")]
        public async Task<ActionResult<ClientDetailResponseDto>> Detail(int id)
        {
            var response = await _clientUseCase.DetailAsync(id);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<bool>> Update([FromBody] ClientUpdateRequestDto request)
        {
            var response = await _clientUseCase.UpdateAsync(request);
            return Ok(response);
        }

        [HttpPut("Remove")]
        public async Task<ActionResult<bool>> Remove([FromBody] ClientRemoveRequestDto request)
        {
            var response = await _clientUseCase.RemoveAsync(request.Id);
            return Ok(response);
        }

        [HttpGet("Inbox")]
        public async Task<ActionResult<PaginationResponseDto<ClientInboxResponseDto>>> Inbox([FromQuery] ClientInboxPaginationRequestDto request)
        {
            var response = await _clientUseCase.InboxAsync(request);
            return Ok(response);
        }

    }
}
