using AstroBhaskar.API.Models;
using AstroBhaskar.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories.Interfaces
{
    public interface ISubscribeNewsLetterRepository : ICrudRepository<SubscribeNewsLetter>
    {
        Task<SubscribeNewsLetter> Get(string Email);
        Task<int> Delete(SubscribeNewsLetter subscribeNewsLetter);
        Task<IEnumerable<MasterCollection>> GetUnsubscribeReason(string collectionName);
    }
}
