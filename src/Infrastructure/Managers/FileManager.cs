using Application.DTOs.AttachmentDto;
using Application.Managers;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Managers
{
    public class FileManager : IFileManager
    {
        private readonly IConfiguration _configuration;

        public FileManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SaveFiles(List<AttachmentFileRequestDto> data)
        {

            var path = _configuration["App:PathFiles"];

            string folderPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var item in data)
            {
                string filePath = Path.Combine(folderPath, item.NewFileName);
                File.WriteAllBytes(filePath, item.File);
            }

        }

        public Stream Get(string name)
        {

            var path = _configuration["App:PathFiles"];
            string folderPath = Path.GetDirectoryName(path);
            string filePath = Path.Combine(folderPath, name);

            return File.OpenRead(filePath);

        }

    }
}
