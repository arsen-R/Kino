using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string NameGenre { get; set; }

        public List<Movie> Movies { get; set; }
        public List<Category> Categories { get; set; }
    }
}
