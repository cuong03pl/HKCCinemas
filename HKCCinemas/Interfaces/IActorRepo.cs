using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IActorRepo
    {
        List<Actor> GetAllActorsByFilmId(int film_id);
        List<ActorViewDTO> GetAllActors();
        Task<bool> CreateActorAsync(ActorDTO actor);
        Task<bool> UpdateActorAsync(int id, ActorDTO actor);
        bool DeleteActor(int id);
        int Count();

        List<ActorViewDTO> SearchActor(QueryObject query);
    }
}
