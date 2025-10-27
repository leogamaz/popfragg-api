using AutoMapper;
using popfragg.Domain.Repositories;
using popfragg.Infrastructure.Exceptions;
using popfragg.Shared.Exceptions;
using popfragg.Domain.Models.Nades;
using popfragg.Domain.Services;
using popfragg.Shared.DTOS.NadesDTO;

namespace popfragg.Services
{
    public class NadesService(INadesRepository _nadesRepository, IMapper _mapper) : INadesService
    {
        public async Task<Nades?> GetNadeByIdAsync(Guid nadeId)
        {
            var nade = await _nadesRepository.GetById(nadeId) ?? throw new NotFoundException("Nade não encontrada", ErrorCodes.NadeNotFound);
            return nade;
        }

        public async Task<Nades> AddNadeAsync(NadeDTO newNade, Guid authorId)
        {
            var nade = _mapper.Map<Nades>(newNade);
            nade.AuthorId = authorId;
            return  await _nadesRepository.AddAsync(nade);
        }
        public async Task<IEnumerable<Nades>> GetAllNadesForUserAsync(Guid userId)
        {

            return await _nadesRepository.GetAllNadesForUserAsync(userId);
        }
    }

}
