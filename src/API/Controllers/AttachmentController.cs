using Application.DTOs.AttachmentDto;
using Application.Exceptions;
using Application.Managers;
using Application.UseCases.AttachmentUseCase;
using Application.Utils;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {

        private readonly IAttachmentUseCase _attachmentUseCase;
        private readonly IUtilManager _utilManager;

        public AttachmentController(IAttachmentUseCase attachmentUseCase, IUtilManager utilManager)
        {
            _attachmentUseCase = attachmentUseCase;
            _utilManager = utilManager;
        }

        [HttpPost("cv")]
        public async Task<ActionResult<AttachmentNewFilesResponseDto>> SaveV([FromForm] List<IFormFile> files)
        {

            var data = ProcessFile(files, EnumUtils.Description(AttachmentsEnum.CV), 5, new List<string> { ".pdf" });

            return Ok(await _attachmentUseCase.SaveAsync(data));

        }

        [HttpGet("cv")]
        public async Task<IActionResult> GetCV(string id)
        {
            var file = await _attachmentUseCase.DownloadAsync(id, EnumUtils.Description(AttachmentsEnum.CV));
            Response.Headers["Content-Disposition"] = "inline; filename=" + file.FileName;
            return File(file.File, file.ContentType);
        }

        [HttpPost("profile")]
        public async Task<ActionResult<List<AttachmentNewFilesResponseDto>>> SaveProfile([FromForm] List<IFormFile> files)
        {

            var data = ProcessFile(files, EnumUtils.Description(AttachmentsEnum.PROFILE), 2, new List<string> { ".jpg", ".jpeg", ".png" });

            return Ok(await _attachmentUseCase.SaveAsync(data));

        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var file = await _attachmentUseCase.DownloadAsync(id, EnumUtils.Description(AttachmentsEnum.PROFILE));
            Response.Headers["Content-Disposition"] = "inline; filename=" + file.FileName;
            return File(file.File, file.ContentType);
        }

        private List<AttachmentFileRequestDto> ProcessFile(List<IFormFile> files, string section, int maxMB, List<string> extensions) {

            if (files == null || !files.Any())
            {
                throw new UserFriendlyException("No se ha adjuntando ningun archivo");
            }

            var data = new List<AttachmentFileRequestDto>();

            foreach (var item in files)
            {

                if (((item.Length / 1024) / 1024) > maxMB)
                {
                    throw new UserFriendlyException($"Existe un archivo que excede los {maxMB} MB");
                }

                var fileExtension = Path.GetExtension(item.FileName);

                if (!extensions.Any(a => string.Equals(a, fileExtension, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new UserFriendlyException($"La extensión {fileExtension} no esta soportado");
                }

            }

            foreach (var item in files)
            {
                var bytes = _utilManager.ConvertStreamToByteArray(item.OpenReadStream());
                data.Add(new AttachmentFileRequestDto()
                {
                    FileName = item.FileName,
                    ContentType = item.ContentType,
                    Section = section,
                    File = bytes
                });

            }

            return data;

        }

    }
}
