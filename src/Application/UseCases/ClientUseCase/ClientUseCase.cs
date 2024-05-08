using Application.DTOs.ClientDto;
using Application.DTOs.CommonDto;
using Application.Exceptions;
using Application.Managers;
using Application.Repositories.Base;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Application.UseCases.ClientUseCase
{
    public class ClientUseCase : IClientUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilManager _utilManager;
        private readonly IConfiguration _configuration;
        private readonly IExcelManager _excelManager;

        public ClientUseCase(IUnitOfWork unitOfWork, IUtilManager utilManager, IConfiguration configuration, IExcelManager excelManager)
        {
            _unitOfWork = unitOfWork;
            _utilManager = utilManager;
            _configuration = configuration;
            _excelManager = excelManager;
        }

        public async Task<bool> CreateAsync(ClientCreateRequestDto request)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {

                    var clientExist = await _unitOfWork.ClientRepository.SingleOrDefaultAsync(x => x.DocumentNumber.Equals(request.DocumentNumber) && x.State);

                    if (clientExist != null)
                    {
                        throw new UserFriendlyException($"El cliente con numero de documento {request.DocumentNumber} ya se encuentra registrado");
                    }

                    ValidInputs(request.DocumentType, request.DocumentNumber);

                    var client = new Client();
                    client.Name = request.Name;
                    client.Lastname = request.Lastname;
                    client.BirthDate = _utilManager.StringToDate(request.BirthDate);
                    client.DocumentType = request.DocumentType;
                    client.DocumentNumber = request.DocumentNumber;
                    client.State = true;
                    await _unitOfWork.ClientRepository.AddAsync(client);
                    await _unitOfWork.SaveChangesAsync();

                    var attachmentCv = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.Id == request.AttachmentCV && x.State && x.ReferenceId == null && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.CV)));

                    if (attachmentCv == null) {
                        throw new UserFriendlyException("No se encuentró el CV adjuntado");
                    }

                    attachmentCv.ReferenceId = client.Id.ToString();
                    await _unitOfWork.AttachmentRepository.UpdateAsync(attachmentCv);

                    var attachmentProfile = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.Id == request.AttachmentProfile && x.State && x.ReferenceId == null && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.PROFILE)));

                    if (attachmentProfile == null)
                    {
                        throw new UserFriendlyException("No se encuentró la foto de perfil adjuntado");
                    }

                    attachmentProfile.ReferenceId = client.Id.ToString();
                    await _unitOfWork.AttachmentRepository.UpdateAsync(attachmentProfile);

                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitTransaction();
                    return true;

                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<ClientDetailResponseDto> DetailAsync(int id)
        {
            var client = await _unitOfWork.ClientRepository.SingleOrDefaultAsync(x => x.Id == id && x.State);

            if (client == null)
            {
                throw new UserFriendlyException("No existe el cliente con id: " + id);
            }

            var attachmentCv = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.State && x.ReferenceId == client.Id.ToString() && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.CV)));

            var attachmentProfile = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.State && x.ReferenceId == client.Id.ToString() && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.PROFILE)));

            var response = new ClientDetailResponseDto();
            response.Id = client.Id;
            response.Name = client.Name;
            response.Lastname = client.Lastname;
            response.BirthDate = _utilManager.DateTimeToDateString(client.BirthDate);
            response.DocumentType = client.DocumentType;
            response.DocumentNumber = client.DocumentNumber;
            response.UrlAttachmentCV = _configuration["App:EndPointCv"] + attachmentCv.Id;
            response.UrlAttachmentProfile = _configuration["App:EndPointProfile"] + attachmentProfile.Id;

            return response;
        }

        public async Task<PaginationResponseDto<ClientInboxResponseDto>> InboxAsync(ClientInboxPaginationRequestDto request)
        {
            var result = await _unitOfWork.ClientRepository.GetInboxPaginationAsync(request);
            foreach (var item in result.Data)
            {
                item.BirthDateFormat = _utilManager.DateTimeToDateString(item.BirthDate);
            }
            return result;
        }

        public async Task<MemoryStream> InboxExcelAsync()
        {
            var data = await _unitOfWork.ClientRepository.GetInboxAsync();

            DataTable dt = new DataTable();

            //Columns
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellidos", typeof(string));
            dt.Columns.Add("Fecha Nacimiento", typeof(string));
            dt.Columns.Add("Tipo de Documento", typeof(string));
            dt.Columns.Add("N° Documento", typeof(string));

            //Body
            foreach (var emp in data)
            {
                emp.BirthDateFormat = _utilManager.DateTimeToDateString(emp.BirthDate);
                dt.Rows.Add(emp.Id, emp.Name, emp.Lastname, emp.BirthDateFormat, emp.DocumentType, emp.DocumentNumber);
            }

            var fileStream = _excelManager.Generate(dt, new List<string> {
                "A1", "B1", "C1", "D1", "E1", "F1"
            });
            return fileStream;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {

                try
                {
                    var client = await _unitOfWork.ClientRepository.SingleOrDefaultAsync(x => x.Id == id && x.State);

                    if (client == null) {
                        throw new UserFriendlyException("No existe el cliente con id: " + id);
                    }

                    client.State = false;
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitTransaction();
                    return true;

                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }

            }
        }

        public async Task<bool> UpdateAsync(ClientUpdateRequestDto request)
        {

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {

                    var client = await _unitOfWork.ClientRepository.SingleOrDefaultAsync(x => x.Id == request.Id && x.State);

                    if (client == null)
                    {
                        throw new UserFriendlyException("No existe el cliente con id: " + request.Id);
                    }

                    client.Name = request.Name;
                    client.Lastname = request.Lastname;
                    client.BirthDate = _utilManager.StringToDate(request.BirthDate);
                    await _unitOfWork.ClientRepository.UpdateAsync(client);

                    var attachmentCvCurrent = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.State && x.ReferenceId == client.Id.ToString() && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.CV)));

                    var attachmentProfileCurrent = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.State && x.ReferenceId == client.Id.ToString() && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.PROFILE)));

                    if (attachmentCvCurrent.Id != request.AttachmentCV) {

                        attachmentCvCurrent.State = false;
                        await _unitOfWork.AttachmentRepository.UpdateAsync(attachmentCvCurrent);

                        var attachmentCv = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.Id == request.AttachmentCV && x.State && x.ReferenceId == null && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.CV)));

                        if (attachmentCv == null)
                        {
                            throw new UserFriendlyException("No se encuentró el CV adjuntado");
                        }

                        attachmentCv.ReferenceId = client.Id.ToString();
                        await _unitOfWork.AttachmentRepository.UpdateAsync(attachmentCv);
                    }

                    if (attachmentProfileCurrent.Id != request.AttachmentProfile)
                    {

                        attachmentProfileCurrent.State = false;
                        await _unitOfWork.AttachmentRepository.UpdateAsync(attachmentProfileCurrent);

                        var attachmentProfile = await _unitOfWork.AttachmentRepository.SingleOrDefaultAsync(x => x.Id == request.AttachmentProfile && x.State && x.ReferenceId == null && x.Section.Equals(EnumUtils.Description(AttachmentsEnum.PROFILE)));

                        if (attachmentProfile == null)
                        {
                            throw new UserFriendlyException("No se encuentró la foto de perfil adjuntado");
                        }

                        attachmentProfile.ReferenceId = client.Id.ToString();
                        await _unitOfWork.AttachmentRepository.UpdateAsync(attachmentProfile);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitTransaction();
                    return true;

                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }

        }

        private void ValidInputs(string documentType, string documentNumber) {

            if (documentType.Equals("DNI") && documentNumber.Length != 8)
            {
                throw new UserFriendlyException("El numero de documento no tiene 8 digitos");
            }
            
            if (documentType.Equals("RUC") && documentNumber.Length != 11)
            {
                throw new UserFriendlyException("El numero de documento no tiene 11 digitos");
            }
            
            if (documentType.Equals("CARNET DE EXTRANJERIA") && documentNumber.Length != 12)
            {
                throw new UserFriendlyException("El numero de documento no tiene 12 digitos");
            }

            if (!(documentType.Equals("DNI") || documentType.Equals("RUC") || documentType.Equals("CARNET DE EXTRANJERIA"))){
                throw new UserFriendlyException("No existe el tipo de documento");
            }

        }

    }
}
