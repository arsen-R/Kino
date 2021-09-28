using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public byte[] Image { get; set; }

        //public List<ActorMovie> ActorMovies { get; set; }
    }
}
