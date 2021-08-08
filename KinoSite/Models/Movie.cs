using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models.Comments;

namespace KinoSite.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slogan { get; set; }
        public double Rating { get; set; }
        public DateTime DateRealise { get; set; }
        public string CountryRealise { get; set; }
        public string Description { get; set; }

        //public int DirectionId { get; set; }
        //public Direction Directions { get; set; }

        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
        public string Age { get; set; }
        public int TimeLenght { get; set; }

        //public List<GenreMovie> GenreMovies { get; set; }
        //public List<ActorMovie> ActorMovies { get; set; }

        public byte[] Image { get; set; }
        public string Video { get; set; }

        public List<MainComment> MainComments { get; set; }
    }
}
