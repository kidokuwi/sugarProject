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
        public string[] displayColumns { get; set; } = ["Id", "uName", "fName", "eMail", "yearBorn", "prefix", "phone", "pass"];
		public IActionResult OnGet()
        {
			DBHelper db = new DBHelper();
			string sqlQuery = $"SELECT * FROM {Utils.DB_USERS_TABLE}";
			DataTableUsers = db.RetrieveTable(sqlQuery, "users");

			return Page();

		}
    }
}
