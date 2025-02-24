using ClassicCarsRazor.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sugarProject.DataModel;

namespace sugarProject.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User user { get; set; }
        public string st {  get; set; }

        public string errorMessage { get; set; }

        public string[] prefixes { get; set; } = { "053", "054", "052", "055", "056", "057", "058", "059" };
        public void OnGet()
        {
        }
    
        public IActionResult OnPost()
        {
            st = "<table>";
            st += "<tr><td colspan='2' style='text-align:center'>Form Data</td></tr>";
            st += $"<tr><td>User Id</td> <td>{user.Id}</td></tr>";
            st += $"<tr><td>User Email</td> <td>{user.eMail}</td></tr>";
            st += $"<tr><td>User fName</td> <td>{user.fName}</td></tr>";
            st += $"<tr><td>User lName</td> <td>{user.lName}</td></tr>";
            st += $"<tr><td>User uName</td> <td>{user.uName}</td></tr>";
            st += $"<tr><td>User pass</td> <td>{user.pass}</td></tr>";
            st += $"<tr><td>User year born</td> <td>{user.yearBorn}</td></tr>";
            st += $"<tr><td>User does sports</td> <td>{user.doesSports}</td></tr>";
            st += $"<tr><td>User eatsWhiteBread</td> <td>{user.eatsWhiteBread}</td></tr>";
            st += $"<tr><td>User eats sugar</td> <td>{user.eatsSugarRegularly}</td></tr>";
            st += $"<tr><td>User prefix</td> <td>{user.prefix}</td></tr>";
            st += $"<tr><td>User phone</td> <td>{user.phone}</td></tr>";
            st += $"<tr><td>User gender</td> <td>{user.gender}</td></tr>";

            st += "</table>";

            DBHelper dB = new DBHelper();
            int numRowsAffected = dB.Insert(user, "users");
            if (numRowsAffected != 1)
            {
                errorMessage = "This email is already registered";
                return Page();
            }
            return RedirectToPage("/index");
        }


    }
}
