using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
