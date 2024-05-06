using Application.DTOs.ClientDto;
using Application.DTOs.CommonDto;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClientRepository : GenericAsyncRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        { }

        private IQueryable<ClientInboxResponseDto> GetInboxQuery()
        {
            return (from c in _context.Clients
                    select new ClientInboxResponseDto
                    {
                        Name = c.Name,
                        Lastname = c.Lastname,
                        BirthDate = c.BirthDate,
                        DocumentType = c.DocumentType,
                        DocumentNumber = c.DocumentNumber
                    }).AsQueryable();
        }

        public async Task<PaginationResponseDto<ClientInboxResponseDto>> GetInboxPaginationAsync(ClientInboxPaginationRequestDto request)
        {
            var dataResult = GetInboxQuery();
            var skip = request.PageSize * (request.PageIndex - 1);
            var data = await dataResult.Skip(skip).Take(request.PageSize).ToListAsync();
            var totalData = await dataResult.CountAsync();
            var rounded = Math.Ceiling(Convert.ToDecimal(totalData) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            return new PaginationResponseDto<ClientInboxResponseDto>
            {
                Count = totalData,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

        }

    }
}
