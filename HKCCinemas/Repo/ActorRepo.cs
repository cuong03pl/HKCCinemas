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
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;

        public ActorRepo(CinemasContext context, IMapper mapper, IWebHostEnvironment evn)
        {
            _context = context;
            _mapper = mapper;
            _evn = evn;
        }
        public int CountActor()
        {
            return _context.Actor.Count();
        }

        public async Task<bool> CreateActorAsync( ActorDTO actor)
        {
            
            if (actor.formFile != null)
            {
                var fileName = actor.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await actor.formFile.CopyToAsync(stream);
                }

                actor.Image = pathToSave;
            }
            var actorMapper = _mapper.Map<Actor>(actor);
            foreach (var item in actor.filmIds)
            {
                var film = _context.Film.Where(film => film.Id == item).FirstOrDefault();
                
                var filmActor = new FilmActor()
                {
                    Film = film,
                    Actor = actorMapper
                };
                _context.FilmActors.Add(filmActor);
            }
            _context.Actor.Add(actorMapper);
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

        public List<Actor> GetAllActorsByFilmId(int film_id)
        {
            var film = _context.Film.Where(f => f.Id == film_id).Include(f => f.filmActors).ThenInclude(f => f.Actor).FirstOrDefault();
            var actorDtos = film.filmActors.Select(f => f.Actor).ToList();
            return actorDtos;
        }
        public List<Actor> GetAllActors()
        {
           return _context.Actor.ToList();
        }
        public async Task<bool> UpdateActorAsync(int id, ActorDTO actor)
        {
            var actorNow = _context.Actor.Where(f => f.Id == id).FirstOrDefault();

            if (actor.formFile != null && actor.formFile.Length > 0)
            {
                var fileName = actor.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await actor.formFile.CopyToAsync(stream);
                }
                actor.Image = pathToSave;
            }
            else
            {
                actor.Image = actorNow.Image;
            }
            actorNow.Name = actor.Name;
            actorNow.Image = actor.Image;
            // xóa các FilmActors cũ
            if (actor.filmIds != null)
            {
                var filmActorOld = _context.FilmActors.Where(c => c.actorId == id);
                foreach (var c in filmActorOld)
                {
                    _context.FilmActors.Remove(c);
                }

                foreach (var item in actor.filmIds)
                {
                    var film = _context.Film.Where(c => c.Id == item).FirstOrDefault();
                    var categoryFilm = new FilmActor()
                    {
                        Actor = actorNow,
                        Film = film
                    };
                    _context.FilmActors.Add(categoryFilm);
                }
            }

            _context.Actor.Update(actorNow);
            _context.SaveChanges();

            return true;
        }

        public List<Actor> SearchActor(string keyword)
        {
            var data = _context.Actor.Where(a => a.Name.Contains(keyword));
            return data.ToList();
        }
    }
}
