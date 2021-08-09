using KinoSite.Areas.Identity.Data;

namespace KinoSite.Models.Comments
{
    public class SubComment : Comment
    {
        public int MainCommentId { get; set; }
        public MainComment MainComment { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string UserName { get; set; }
    }
}
