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
    public class IndexModel : PageModel
    {
        private readonly personal_Blog.Data.ApplicationDbContext _context;

        public IndexModel(personal_Blog.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Publication> Publication { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Publication = await _context.Publication.ToListAsync();
        }
    }
}
