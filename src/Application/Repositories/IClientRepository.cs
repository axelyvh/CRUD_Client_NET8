using Application.DTOs.ClientDto;
using Application.DTOs.CommonDto;
using Application.Repositories.Base;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IClientRepository : IGenericAsyncRepository<Client>
    {
        Task<List<ClientInboxResponseDto>> GetInboxAsync();
        Task<PaginationResponseDto<ClientInboxResponseDto>> GetInboxPaginationAsync(ClientInboxPaginationRequestDto request);
    }
}
