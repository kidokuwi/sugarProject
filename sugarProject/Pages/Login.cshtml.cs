using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sugarProject.DataModel;
using ClassicCarsRazor.DataModel;
using System.Data;

namespace sugarProject.Pages
{
    public class LoginModel : PageModel
    {
		public User user { get; set; }

        public string? login_email { get; set; }
		public string? login_password { get; set; }

        public IActionResult OnPost()
		{
			DBHelper db = new DBHelper();
			DataTable userTable;
			string sqlQuery = $"SELECT * FROM {Utils.DB_USERS_TABLE} WHERE eMail = '{login_email}' AND pass = '{login_password}'";
			userTable = db.RetrieveTable(sqlQuery, "users");

			if (userTable.Rows.Count != 1)
			{
				return Page();
			}
			else
			{
				return RedirectToPage("/index");
				VisitorService VS = ServiceProviderAccessor.ServiceProvider.GetRequiredService<VisitorService>();
				VS.IncrementVisitorCount();
			}

			}
		public void OnGet()
        {
        }
    }
}
