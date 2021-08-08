using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Areas.Identity.Data;

namespace KinoSite.Models.Comments
{
    public class MainComment : Comment
    {
        public List<SubComment> SubComments { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string UserName { get; set; }
    }
}
