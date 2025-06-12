using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using personal_Blog.Data;
using personal_Blog.Models;

namespace personal_Blog.Pages.Blogpages
{
    public class DetailsModel : PageModel
    {
        private readonly personal_Blog.Data.ApplicationDbContext _context;

        public DetailsModel(personal_Blog.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Publication Publication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.FirstOrDefaultAsync(m => m.Id == id);

            if (publication is not null)
            {
                Publication = publication;

                return Page();
            }

            return NotFound();
        }
    }
}
