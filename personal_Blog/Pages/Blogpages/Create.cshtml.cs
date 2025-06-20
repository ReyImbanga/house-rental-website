using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using personal_Blog.Data;
using personal_Blog.Models;

namespace personal_Blog.Pages.Blogpages
{
    public class CreateModel : PageModel
    {
        private readonly personal_Blog.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(personal_Blog.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publication Publication { get; set; } = default!;
        [BindProperty]
        public IFormFile? ProfileImage { get; set; }

        [BindProperty]
        public List<IFormFile>? AdditionalImages { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string wwwRootPath = _environment.WebRootPath;
            // Sauvegarder l'image de profil
            if (ProfileImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                string filePath = Path.Combine(wwwRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                Publication.ProfileImagePath = "/images/" + fileName;
            }

            // Sauvegarder les images supplémentaires
            if (AdditionalImages != null && AdditionalImages.Count > 0)
            {
                List<string> imagePaths = new List<string>();

                foreach (var image in AdditionalImages)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(wwwRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    imagePaths.Add("/images/" + fileName);
                }

                Publication.ImagePaths = JsonSerializer.Serialize(imagePaths); // ou string.Join(",", imagePaths)
            }

            _context.Publication.Add(Publication);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
