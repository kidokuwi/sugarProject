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
		public string? email { get; set; }
		public string? password { get; set; }
		public string? fName { get; set; }


		public void OnGet()
		{
			DBHelper db = new DBHelper();
			DataTable userTable;
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
				email = userTable.Rows[0]["eMail"].ToString();
				password = userTable.Rows[0]["pass"].ToString();
				fName = userTable.Rows[0]["fName"].ToString();
			}


		}
		public IActionResult OnPost()
		{
			DBHelper db = new DBHelper();
			string sqlQuery = $"UPDATE {Utils.DB_USERS_TABLE} SET eMail = '{email}', pass = '{password}', fName = '{fName}' WHERE Id = '{id}'";
			db.Update(sqlQuery, "users");
			return RedirectToPage("/index");

		}
	}
}
