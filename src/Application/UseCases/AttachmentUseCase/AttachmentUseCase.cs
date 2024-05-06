using Application.DTOs.AttachmentDto;
using Application.Exceptions;
using Application.Managers;
using Application.Repositories.Base;
using Domain.Entities;

namespace Application.UseCases.AttachmentUseCase
{
    public class AttachmentUseCase : IAttachmentUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public AttachmentUseCase(IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public async Task<List<AttachmentNewFilesResponseDto>> SaveAsync(List<AttachmentFileRequestDto> request)
        {

            var response = new List<AttachmentNewFilesResponseDto>();

            foreach (var item in request)
            {
                var attachment = new Attachment();
                attachment.Id = Guid.NewGuid().ToString();
                attachment.Filename = item.FileName;
                attachment.Section = item.Section;
                attachment.ContentType = item.ContentType;
                attachment.State = true;
                await _unitOfWork.AttachmentRepository.AddAsync(attachment);
                await _unitOfWork.SaveChangesAsync();

                item.NewFileName = attachment.Id;

                response.Add(new AttachmentNewFilesResponseDto
                {
                    Id = attachment.Id,
                    FileName = attachment.Filename
                });
            }

            _fileManager.SaveFiles(request);

            return response;

        }

        public async Task<AttachmentDownloadFileResponseDto> DownloadAsync(string id, string section)
        {
            var attachment = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.Id.Equals(id) && x.Section.Equals(section) && x.State);

            if (attachment == null)
            {
                throw new NotFoundException(nameof(Attachment), id);
            }

            var file = _fileManager.Get(attachment.Id);

            return new AttachmentDownloadFileResponseDto
            {
                File = file,
                FileName = attachment.Filename,
                ContentType = attachment.ContentType
            };

        }

    }
}
