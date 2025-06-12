using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace personal_Blog.Pages.PagesNote
{
    public class IndexModel : PageModel
    {
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
