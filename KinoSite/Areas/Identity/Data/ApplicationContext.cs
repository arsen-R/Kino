using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Areas.Identity.Data;
using KinoSite.Models;
using KinoSite.Models.Comments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KinoSite.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        //public DbSet<Direction> Directions { get; set; }
        //public DbSet<Genre> Genres { get; set; }
        //public DbSet<Actor> Actors { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<GenreMovie> GenreMovies { get; set; }
        //public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<GenreMovie>()
        // .HasKey(bc => new { bc.MovieId, bc.GenreId });
        //    modelBuilder.Entity<GenreMovie>()
        //        .HasOne(bc => bc.Movie)
        //        .WithMany(b => b.GenreMovies)
        //        .HasForeignKey(bc => bc.MovieId);
        //    modelBuilder.Entity<GenreMovie>()
        //        .HasOne(bc => bc.Genre)
        //        .WithMany(c => c.GenreMovies)
        //        .HasForeignKey(bc => bc.GenreId);

        //    modelBuilder.Entity<ActorMovie>()
        // .HasKey(bc => new { bc.MovieId, bc.ActorId });
        //    modelBuilder.Entity<ActorMovie>()
        //        .HasOne(bc => bc.Movie)
        //        .WithMany(b => b.ActorMovies)
        //        .HasForeignKey(bc => bc.MovieId);
        //    modelBuilder.Entity<ActorMovie>()
        //        .HasOne(bc => bc.Actor)
        //        .WithMany(c => c.ActorMovies)
        //        .HasForeignKey(bc => bc.ActorId);
        //}
    }
}
