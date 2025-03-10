using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sugarProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

      

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
			VisitorService VS = ServiceProviderAccessor.ServiceProvider.GetRequiredService<VisitorService>();
			VS.IncrementVisitorCount();
        }
    }
}
