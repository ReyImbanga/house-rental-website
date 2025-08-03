using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using personal_Blog.Data;
using personal_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace personal_Blog.Pages.Blogpages
{
    public class EditModel : PageModel
    {
        private readonly personal_Blog.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(personal_Blog.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Publication Publication { get; set; } = default!;
        [BindProperty]
        public IFormFile? ProfileImage { get; set; }
        [BindProperty]
        public List<IFormFile>? AdditionalImages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication =  await _context.Publication.FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }
            Publication = publication;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var publicationToUpdate = await _context.Publication.FindAsync(Publication.Id);
            if (publicationToUpdate == null) { 
                return NotFound();
            }

            publicationToUpdate.Title = Publication.Title;
            publicationToUpdate.Description = Publication.Description;
            publicationToUpdate.TypeHouse = Publication.TypeHouse;
            publicationToUpdate.CreatedAt = Publication.CreatedAt;

            /*_context.Attach(Publication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(Publication.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/
            string uploads = Path.Combine(_environment.WebRootPath, "uploads");
            // Create the folder if it does not exist
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            // Remplacement image de profil
            if (ProfileImage != null)
            {
                if (!string.IsNullOrEmpty(publicationToUpdate.ProfileImagePath))
                {
                    string oldImagePath = Path.Combine(_environment.WebRootPath, publicationToUpdate.ProfileImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                string path = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                publicationToUpdate.ProfileImagePath = "/uploads/" + fileName;
            }

            // Ajout de nouvelles images supplémentaires (ajoute aux anciennes ou remplace selon ton choix)
            if (AdditionalImages != null && AdditionalImages.Count > 0)
            {
                // Delete old images
                if (!string.IsNullOrEmpty(publicationToUpdate.ImagePaths))
                {
                    var oldImages = JsonSerializer.Deserialize<List<string>>(publicationToUpdate.ImagePaths);
                    if (oldImages != null)
                    {
                        foreach (var img in oldImages)
                        {
                            string oldImagePath = Path.Combine(_environment.WebRootPath, img.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                    }
                }

                // Save the new images
                List<string> imagePaths = new List<string>();

                foreach (var file in AdditionalImages)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imagePaths.Add("/uploads/" + fileName);
                }

                // Remplacer toutes les anciennes images :
                publicationToUpdate.ImagePaths = JsonSerializer.Serialize(imagePaths);
            }

            try
            {
                await _context.SaveChangesAsync(); // ← maintenant à la fin, une seule fois
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(Publication.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PublicationExists(int id)
        {
            return _context.Publication.Any(e => e.Id == id);
        }
    }
}
