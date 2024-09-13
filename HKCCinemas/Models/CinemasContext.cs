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
           
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Cinemas)
                .WithMany(c => c.Rooms)
                .HasForeignKey(r => r.CinemasId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Film)
                .WithMany()
                .HasForeignKey(s => s.FilmId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Room)
                .WithMany()
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Cinemas)
                .WithMany()
                .HasForeignKey(s => s.CinemasId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.ShowDate)
                .WithMany()
                .HasForeignKey(s => s.ShowDateId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Cinemas>()
                .HasOne(c => c.CinemasCategory)
                .WithMany()
                .HasForeignKey(c => c.CinemasCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                 .HasIndex(u => u.UserName).IsUnique();
                  modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

        }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryFilm> CategoryFilm { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<BookingUser> BookingUsers { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Cinemas> Cinemas { get; set; }
        public DbSet<CinemasCategory> CinemasCategories { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ShowDate> ShowDates { get; set; }
        public DbSet<Favourite>  Favourites{ get; set; }
        public DbSet<SeatStatus>  SeatStatuses{ get; set; }


    }
}
