using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DogForum.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData] // property is included in download of personal data.
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string location { get; set; } = string.Empty;

        [PersonalData]
        public string ImageFilename { get; set; } = "vecteezy_profile-icon-design-vector_5544718.jpg";

        [PersonalData]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
