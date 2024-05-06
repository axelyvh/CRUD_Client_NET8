using Application.DTOs.AttachmentDto;

namespace Application.Managers
{
    public interface IFileManager
    {
        void SaveFiles(List<AttachmentFileRequestDto> data);
        Stream Get(string name);
    }
}
