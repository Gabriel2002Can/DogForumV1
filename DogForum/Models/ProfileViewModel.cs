using DogForum.Data;

namespace DogForum.Models
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Discussions> Discussions { get; set; }
    }
}
