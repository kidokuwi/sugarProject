using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sugarProject.Pages
{
    public class mainModel : PageModel
    {
        public void OnGet()
        {
			if (HttpContext.Session.GetString("fName") == null)
			{
				;
				ViewData["UserName"] = "Guest";
			}
			else
			{
				ViewData["UserName"] = HttpContext.Session.GetString("fName");
			}
		}
    }
}
