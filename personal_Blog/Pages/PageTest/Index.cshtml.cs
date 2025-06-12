using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace personal_Blog.Pages.PageTest
{
    public class IndexModel : PageModel
    {
        public IFormFile ProfileImage { get; set; }
        public List<IFormFile> PostImages { get; set; }
        public List<string> ImagePaths { get; set; } = new();
        public async Task<IActionResult> OnPostAsync()
        {
            var uploadsFolder = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            if (ProfileImage != null)
            {
                var profileFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, profileFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                ImagePaths.Add("/images/" + profileFileName);
            }
            if (PostImages != null && PostImages.Count > 0)
            {
                foreach (var image in PostImages)
                {
                    var postFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(uploadsFolder, postFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    ImagePaths.Add("/images/" + postFileName);
                }
            }

            return Page(); // pour r?afficher les images apr?s upload
        }
        public void OnGet()
        {
        }
    }
}
