using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string SurnameActor { get; set; }
        public string NameActor { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
