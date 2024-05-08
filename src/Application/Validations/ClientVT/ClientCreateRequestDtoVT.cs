using Application.DTOs.ClientDto;
using FluentValidation;

namespace Application.Validations.ClientVT
{
    public class ClientCreateRequestDtoVT : AbstractValidator<ClientCreateRequestDto>
    {
        public ClientCreateRequestDtoVT()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Lastname).NotEmpty().NotNull();
            RuleFor(x => x.BirthDate).NotEmpty().NotNull();
            RuleFor(x => x.DocumentType).NotEmpty().NotNull();
            RuleFor(x => x.DocumentNumber).NotEmpty().NotNull().Must(BeNumeric);
            RuleFor(x => x.AttachmentCV).NotEmpty().NotNull();
            RuleFor(x => x.AttachmentProfile).NotEmpty().NotNull();
        }

        private bool BeNumeric(string documentNumber)
        {
            return long.TryParse(documentNumber, out _);
        }
    }
}
