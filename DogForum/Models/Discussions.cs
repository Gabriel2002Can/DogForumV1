using System.ComponentModel.DataAnnotations;

namespace DogForum.Models
{
    public class Discussions
    {
        public int DiscussionsId { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public List<Comments>? Comments { get; set; }
    }
}
