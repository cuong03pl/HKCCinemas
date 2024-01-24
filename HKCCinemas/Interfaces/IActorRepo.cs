using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IActorRepo
    {
        List<Actor> GetAllActors(int film_id); 
        bool CreateActor(int filmId, Actor actor);
        bool UpdateActor(Actor actor);
        bool DeleteActor(int id);
        int CountActor();
    }
}
