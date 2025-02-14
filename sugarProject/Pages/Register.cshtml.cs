using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sugarProject.DataModel;

namespace sugarProject.Pages
{
	public class RegisterModel : PageModel
	{
		[BindProperty]
		public User user { get; set; }
		public string st { get; set; }
		public void OnGet()
		{
		}
		public void OnPost()
		{
			st = "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%;'>";
			st += "<tr><th colspan='2' style='text-align:center; background-color:#f2f2f2;'>Form Data</th></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Id</td> <td> {user.Id}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Email</td> <td> {user.eMail}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User First Name</td> <td> {user.fName}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Last Name</td> <td> {user.lName}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Username</td> <td> {user.uName}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Password</td> <td> {user.pass}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Year Born</td> <td> {user.yearBorn}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Does Sports</td> <td> {user.doesSports}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Eats White Bread</td> <td> {user.eatsWhiteBread}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Eats Sugar Regularly</td> <td> {user.eatsSugarRegularly}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Prefix</td> <td> {user.prefix}</td></tr>";
			st += $"<tr><td style='font-weight:bold;'>User Phone</td> <td> {user.phone}</td></tr>";
			st += "</table>";
		}
	}
}
