using ClassicCarsRazor.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sugarProject.DataModel;
using System.Data;
using System.Reflection.Metadata;

namespace sugarProject.Pages
{
    public class AdminModel : PageModel
    {
		public DataTable? DataTableUsers { get; set; }

		public string filterColumn { get; set; }
		public string filterValue { get; set; }

		public string[] displayColumns { get; set; } = ["Id", "uName","lName", "fName", "eMail", "yearBorn", "prefix", "phone", "pass"];
		public IActionResult OnGet()
		{
			DBHelper db = new DBHelper();
			string sqlQuery = $"SELECT * FROM {Utils.DB_USERS_TABLE}";

			DataTableUsers = db.RetrieveTable(sqlQuery, "users");

			return Page();

		}
		public IActionResult OnPost()
		{
			DBHelper db = new DBHelper();
			string sqlQuery = $"SELECT * FROM {Utils.DB_USERS_TABLE}";
			if (!string.IsNullOrEmpty(filterColumn) && !string.IsNullOrEmpty(filterValue))
			{
				sqlQuery += $" WHERE [{filterColumn}] LIKE '%{filterValue}%'\r\n";
			}
			
			DataTableUsers = db.RetrieveTable(sqlQuery, "users");
			return Page();
		}
		
    }
}
