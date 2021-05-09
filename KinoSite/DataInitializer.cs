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
        public static void Initialize(MovieContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { NameCategory = "Movie" },
                    new Category { NameCategory = "Serial" },
                    new Category { NameCategory = "Cartoon" }
                    );
                context.SaveChanges();
            }
        }
    }
}
