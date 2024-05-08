using Application.DTOs.ClientDto;
using Application.DTOs.CommonDto;

namespace Application.UseCases.ClientUseCase
{
    public interface IClientUseCase
    {
        Task<PaginationResponseDto<ClientInboxResponseDto>> InboxAsync(ClientInboxPaginationRequestDto request);
        Task<bool> CreateAsync(ClientCreateRequestDto request);
        Task<ClientDetailResponseDto> DetailAsync(int id);
        Task<bool> UpdateAsync(ClientUpdateRequestDto request);
        Task<bool> RemoveAsync(int id);
        Task<MemoryStream> InboxExcelAsync();
    }
}
