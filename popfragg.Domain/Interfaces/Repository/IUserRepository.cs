

using popfragg.Domain.Entities;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {

        public Task<UserEntitie> GetUserBySteamId(string steamId);
    }
}
