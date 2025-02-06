namespace DogForum.Models
{
    public class Comments
    {
        public int CommentsId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int DiscussionsId { get; set; }
        public Discussions? Discussions { get; set; }

    }
}
