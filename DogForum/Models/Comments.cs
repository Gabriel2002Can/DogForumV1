using DogForum.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogForum.Models
{
    public class Comments
    {
        public int CommentsId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int DiscussionsId { get; set; }
        public Discussions? Discussions { get; set; }

        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

    }
}
