using ClassicCarsRazor.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sugarProject.DataModel;

namespace sugarProject.Pages
{
    public class DeleteUserModel : PageModel
    {
		public IActionResult OnGet(int Id)
		{
			if (Id > 0)
			{
				DBHelper dBHelper = new DBHelper();
				string sqlQuery = $"DELETE FROM {Utils.DB_USERS_TABLE} WHERE Id = {Id}";
				int numRowsAffected = dBHelper.ExecuteNonQuery(sqlQuery);
				if (numRowsAffected != 1)
				{
					return RedirectToPage("/Admin");
				}
			}
			return RedirectToPage("/Admin");
		}

	}
}
