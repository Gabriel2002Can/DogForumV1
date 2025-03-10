
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DogForum.Data;

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

        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
