using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Direction
    {
        public int Id { get; set; }
        public string SurnameDirection { get; set; }
        public string NameDirection { get; set; }

        public List<Movie> Movie { get; set; }
    }
}
