using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public byte[] Image { get; set; }

        //public string Biography { get; set; }
        //public DateTime BirthYear { get; set; }
        //public DateTime DeathYear { get; set; }

        //public List<ActorMovie> ActorMovies { get; set; }
    }
}
