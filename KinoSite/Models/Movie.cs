using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Direction Directions { get; set; }

        public string Age { get; set; }
        public int TimeLenght { get; set; }

        public List<Genre> Genres { get; set; }
        public List<Actor> MainRoles { get; set; }

        public byte[] Image { get; set; }
        public byte[] Video { get; set; }
    }
}
