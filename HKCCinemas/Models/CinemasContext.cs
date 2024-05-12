using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HKCCinemas.Models
{
    public class CinemasContext : IdentityDbContext<User>
    {
        public CinemasContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryFilm>().HasKey(cf => new { cf.CategoryId, cf.FilmId });
            modelBuilder.Entity<CategoryFilm>().
                HasOne(cf => cf.Category).
                WithMany(cf => cf.categoryFilms).
                HasForeignKey(cf => cf.CategoryId);

            modelBuilder.Entity<CategoryFilm>().
                HasOne(cf => cf.Film).
                WithMany(cf => cf.categoryFilms).
                HasForeignKey(cf => cf.FilmId);

            modelBuilder.Entity<FilmActor>().HasKey(fa => new { fa.filmId, fa.actorId });
            modelBuilder.Entity<FilmActor>().
                HasOne(fa => fa.Film).
                WithMany(fa => fa.filmActors).
                HasForeignKey(fa => fa.filmId);

            modelBuilder.Entity<FilmActor>().
                HasOne(fa => fa.Actor).
                WithMany(fa => fa.filmActors).
                HasForeignKey(fa => fa.actorId);

        }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryFilm> CategoryFilm { get; set; }
        public DbSet<Cinemas> Cinemas { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<ShowTimes> ShowTime { get; set; }
        public DbSet<Time> Time { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
    }
}
