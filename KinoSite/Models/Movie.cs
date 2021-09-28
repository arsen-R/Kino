using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KinoSite.Models.Comments;

namespace KinoSite.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slogan { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public DateTime DateRealise { get; set; }
        [Required]
        public string CountryRealise { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int DirectionId { get; set; }
        public Direction Directions { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public int TimeLenght { get; set; }

        //public List<GenreMovie> GenreMovies { get; set; }
        //public List<ActorMovie> ActorMovies { get; set; }

        public byte[] Image { get; set; }
        [Required]
        public string Video { get; set; }
        [Required]
        public string VideoLink { get; set; } = "https://28.svetacdn.in/MrJszJIePquE/";
        public List<MainComment> MainComments { get; set; }
    }
}
