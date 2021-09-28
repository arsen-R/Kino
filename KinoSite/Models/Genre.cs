using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string NameGenre { get; set; }

        //public List<GenreMovie> GenreMovies { get; set; }
    }
}
