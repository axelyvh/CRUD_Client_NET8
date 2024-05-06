using Application.DTOs.ClientDto;
using FluentValidation;

namespace Application.Validations.ClientVT
{
    public class ClientRemoveRequestDtoVT : AbstractValidator<ClientUpdateRequestDto>
    {
        public ClientRemoveRequestDtoVT()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
