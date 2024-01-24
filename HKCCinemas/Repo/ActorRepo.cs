using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class ActorRepo : IActorRepo
    {
        private readonly CinemasContext _context;
        private readonly Mapper _mapper;

        public ActorRepo(CinemasContext context)
        {
            _context = context;
        }
        public int CountActor()
        {
            return _context.Actor.Count();
        }

        public bool CreateActor( int filmId,Actor actor)
        {
            var film = _context.Film.Where(film => film.Id == filmId).FirstOrDefault();

            var filmActor = new FilmActor()
            {
                Film = film,
                Actor = actor
            };
            _context.FilmActors.Add(filmActor);
            _context.Actor.Add(actor);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteActor(int id)
        {
            _context.Actor.Remove(GetActorById(id));
            _context.SaveChanges(); 
            return true;
        }

        public Actor GetActorById(int id)
        {
            return _context.Actor.Where(a => a.Id == id).FirstOrDefault();
        }

        public List<Actor> GetAllActors(int film_id)
        {
            var film = _context.Film.Where(f => f.Id == film_id).Include(f => f.filmActors).ThenInclude(f => f.Actor).FirstOrDefault();
            var actorDtos = film.filmActors.Select(f => f.Actor).ToList();
            return actorDtos;
        }

        public bool UpdateActor(Actor actor)
        {
            _context.Entry(actor).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
    }
}
