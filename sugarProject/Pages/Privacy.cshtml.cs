using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sugarProject.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

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
