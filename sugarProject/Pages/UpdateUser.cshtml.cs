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
			if (HttpContext.Session.GetString("fName") == null)
			{
				ViewData["UserName"] = "Guest";
				Response.Cookies.Delete("fName");
			}
			else
			{
				ViewData["UserName"] = HttpContext.Session.GetString("fName");
				id = HttpContext.Session.GetString("id");
				string sqlQuery = $"SELECT * FROM {Utils.DB_USERS_TABLE} WHERE Id = '{id}'";
				userTable = db.RetrieveTable(sqlQuery, "users");
				password = userTable.Rows[0]["pass"].ToString();
				email = userTable.Rows[0]["eMail"].ToString();
				fName = userTable.Rows[0]["fName"].ToString();
			}


		}
		public IActionResult OnPost()
		{
			DBHelper db = new DBHelper();
			user = db.GetUserById(HttpContext.Session.GetString("id"));
			user.eMail = "idoooooooo@gmail.com";
			user.fName = "IDOOOO";
			user.pass = "1234Aa123";
			
			db.Update(user, "users");
			return RedirectToPage("/index");
		}
	}
}
