using Application.DTOs.AttachmentDto;

namespace Application.UseCases.AttachmentUseCase
{
    public interface IAttachmentUseCase
    {
        Task<List<AttachmentNewFilesResponseDto>> SaveAsync(List<AttachmentFileRequestDto> request);
        Task<AttachmentDownloadFileResponseDto> DownloadAsync(string id, string section);
    }
}
