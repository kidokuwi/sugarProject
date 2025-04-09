using ClassicCarsRazor.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sugarProject.DataModel;
using System.Data;

namespace sugarProject.Pages
{

	public class UpdateUserModel : PageModel
	{
		public User user { get; set; }

		private string id;

		[BindProperty]
		public string? email { get; set; }
		[BindProperty]
		public string? password { get; set; }
		[BindProperty]
		public string? fName { get; set; }


		public void OnGet()
		{
			
			DBHelper db = new DBHelper();
			DataTable userTable;
			user = db.GetUserById(HttpContext.Session.GetString("id"));
		


		}
		public IActionResult OnPost()
		{
			DBHelper db = new DBHelper();
			user = db.GetUserById(HttpContext.Session.GetString("id"));
			user.eMail = email;
			user.pass = password;
			user.fName = fName;

			HttpContext.Session.SetString("fName", fName);
			Response.Cookies.Append("fName", HttpContext.Session.GetString("fName"));

			db.Update(user, "users");
			return RedirectToPage("/index");
		}
	}
}
