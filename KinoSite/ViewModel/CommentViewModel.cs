using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.ViewModel
{
    public class CommentViewModel
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int MainCommentId{get;set;}
        public string Message { get; set; }

    }
}
