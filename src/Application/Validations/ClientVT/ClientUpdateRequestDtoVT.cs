using Application.DTOs.ClientDto;
using FluentValidation;

namespace Application.Validations.ClientVT
{
    public class ClientUpdateRequestDtoVT : AbstractValidator<ClientUpdateRequestDto>
    {
        public ClientUpdateRequestDtoVT()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Lastname).NotEmpty().NotNull();
            RuleFor(x => x.BirthDate).NotEmpty().NotNull();
            RuleFor(x => x.AttachmentCV).NotEmpty().NotNull();
            RuleFor(x => x.AttachmentProfile).NotEmpty().NotNull();
        }
    }
}
