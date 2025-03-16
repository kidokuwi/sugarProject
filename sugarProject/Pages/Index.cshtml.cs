using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sugarProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string fName { get; set; }
		public string cookieValue { get; set; }

		public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
			cookieValue = Request.Cookies["fName"];
			VisitorService VS = ServiceProviderAccessor.ServiceProvider.GetRequiredService<VisitorService>();
			VS.IncrementVisitorCount();
            if (HttpContext.Session.GetString("fName") == null)
			{
				fName = "Guest";
				ViewData["UserName"] = fName;
				Response.Cookies.Delete("fName");
			}
			else
			{
				fName = HttpContext.Session.GetString("fName");
				ViewData["UserName"] = fName;
			}
		}
    }
}
