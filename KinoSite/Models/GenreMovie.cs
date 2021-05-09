using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class GenreMovie
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
