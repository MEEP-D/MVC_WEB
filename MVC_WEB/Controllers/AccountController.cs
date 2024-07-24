using System.Linq;
using System.Web.Mvc;
using MVC_WEB.Helpers;
using MVC_WEB.Models;

public class AccountController : Controller
{
    private DBSchoolsContext db = new DBSchoolsContext();

    // GET: Account/Login
    public ActionResult Login()
    {
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                // Set authentication cookie or session here
                Session["UserID"] = user.ID;
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var hashedPassword = PasswordHelper.HashPassword(model.Password);
            var user = new ViewUserControl
            {
                Username = model.Username,
                Password = hashedPassword
            };

            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login");
        }
        return View(model);
    }

    // GET: Account/Logout
    public ActionResult Logout()
    {
        Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
