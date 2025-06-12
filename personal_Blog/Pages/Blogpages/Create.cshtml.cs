using System;
using System.Collections.Generic;
using System.Linq;
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

        public CreateModel(personal_Blog.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publication Publication { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Publication.Add(Publication);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
