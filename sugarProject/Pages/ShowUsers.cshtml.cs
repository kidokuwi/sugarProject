using sugarProject.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using ClassicCarsRazor.DataModel;
namespace sugarProject.Pages
{
    public class ShowUsersModel : PageModel
    {
		public DataTable? DataTableUsers { get; set; }
        public string[] displayColumns { get; set; } = ["Id", "uName", "fName", "eMail", "yearBorn", "prefix", "phone", "pass", "gender"];
		public IActionResult OnGet()
        {
			DBHelper db = new DBHelper();
			if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToPage("/index");
            }
            string sqlQuery = $"SELECT * FROM {Utils.DB_USERS_TABLE}";
			DataTableUsers = db.RetrieveTable(sqlQuery, "users");
			if (HttpContext.Session.GetString("fName") == null)
			{
				;
				ViewData["UserName"] = "Guest";
			}
			else
			{
				ViewData["UserName"] = HttpContext.Session.GetString("fName");
			}
			return Page();

		}
    }
}
