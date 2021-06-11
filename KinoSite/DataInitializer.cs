using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Data;
using KinoSite.Models;
using KinoSite.Areas.Identity.Data;
namespace KinoSite
{
    public class DataInitializer
    {
        // movie, anime, tv-series, show-tv-series, anime-tv-series
        public static void Initialize(MovieContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { NameCategory = "movie" },
                    new Category { NameCategory = "anime" },
                    new Category { NameCategory = "tv-series" },
                    new Category { NameCategory = "show-tv-series" },
                    new Category { NameCategory = "anime-tv-series" }
                    );
            }
        }
    }
}
