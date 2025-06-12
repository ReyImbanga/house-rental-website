using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace personal_Blog.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {

    }
    //[TempData]
    //public string ?Role {  get; set; }
    public IActionResult OnPostGoAsSeller()
    {
        HttpContext.Session.SetString("Role", "vendeur");
        //Role = HttpContext.Session.GetString("Role");
        if(User.Identity.IsAuthenticated)
        {
            return Redirect("/PagesNote/Index");
        }
        else
        {
            HttpContext.Session.SetString("Role", "vendeur");
            return Redirect("/Identity/Account/Login?returnUrl=/PagesNote/Index");
        }

        //return Redirect("/Identity/Account/Login");
    }
    public IActionResult OnPostGoAsClient() 
    {
        //Role = "client";
        HttpContext.Session.SetString("Role", "client");
        return RedirectToPage("/PagesNote/Index");
    } 
}
