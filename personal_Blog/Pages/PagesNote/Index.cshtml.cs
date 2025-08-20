using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using personal_Blog.Data;
using personal_Blog.Models;

namespace personal_Blog.Pages.PagesNote
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Publication> Publications { get; set; }
        //[TempData]
        public string ?Role {  get; set; }
        /*public IActionResult OnGet()
        {
            Role = HttpContext.Session.GetString("Role");
            if (Role == "vendeur" && !User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            // Sinon, on affiche la page
            return Page();
        }*/
        public void OnGet()
        {
            Publications = _context.Publication
.Include(p => p.User) // charger aussi l’auteur
.OrderByDescending(p => p.CreatedAt)
.ToList();

            Role = HttpContext.Session.GetString("Role");
            if (Role == "vendeur" && !User.Identity.IsAuthenticated)
            {
                Redirect("/Identity/Account/Login");
            }
            if(Role == "vendeur")
            {
                Page();
            }
            // Sinon, on affiche la page
            //Page();

        }
    }
}
