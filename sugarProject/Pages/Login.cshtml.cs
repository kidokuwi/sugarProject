using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sugarProject.DataModel;
using ClassicCarsRazor.DataModel;
using System.Data;
using System;

namespace sugarProject.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public string? login_email { get; set; }

		[BindProperty]
		public string? login_password { get; set; }

		public IActionResult OnPost()
		{
			LoginService LS = ServiceProviderAccessor.ServiceProvider.GetRequiredService<LoginService>();
			DBHelper db = new DBHelper();
			DataTable userTable;
			string sqlQuery = $"SELECT * FROM {Utils.DB_USERS_TABLE} WHERE eMail = '{login_email}' AND pass = '{login_password}'";
			userTable = db.RetrieveTable(sqlQuery, "users");

			if (userTable.Rows.Count < 1)
			{
				Response.Cookies.Delete("fName");
				return RedirectToPage("/Register");

			}

			LS.IncrementloginCount();
			string fName = userTable.Rows[0]["fName"].ToString();
			HttpContext.Session.SetString("fName", fName);
			Response.Cookies.Append("fName", HttpContext.Session.GetString("fName"));
			return RedirectToPage("/index");
			
		}
		public void OnGet()
        {
			LoginService LS = ServiceProviderAccessor.ServiceProvider.GetRequiredService<LoginService>();

			if (HttpContext.Session.GetString("fName") == null)
			{
				ViewData["UserName"] = "Guest";
				Response.Cookies.Delete("fName");
			}
			else
			{
				ViewData["UserName"] = HttpContext.Session.GetString("fName");	
			}
		}
    }
}
