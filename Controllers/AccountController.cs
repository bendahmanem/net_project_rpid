using System.Security.Claims;
using ASPBookProject.Data;
using ASPBookProject.Models;
using ASPBookProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using AspNetCoreGeneratedDocument;

public class AccountController : Controller
{


    private readonly UserManager<Medecin> _userManager;
    private IPasswordHasher<Medecin> passwordHasher;


    private readonly SignInManager<Medecin> _signInManager; // permet de gerer la connexion et la deconnexion des utilisateurs, nous est fourni par ASP.NET Core Identity



    // public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    // {
    //     _signInManager = signInManager;
    //     _userManager = userManager;
    // }
    public AccountController(SignInManager<Medecin> signInManager, UserManager<Medecin> userManager, IPasswordHasher<Medecin> passwordHash)
    {
        _signInManager = signInManager; // Signin manager est injecté dans le constructeur,
        // c'est une classe generique qui prend en parametre ApplicationUser
        _userManager = userManager;
        passwordHasher = passwordHash;

    }

    public IActionResult Login()
    {
        return View(); // Affiche la vue Login
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Erreur lors du login");
        }

        return View(model);
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        // Retourne la vue Logout qui affichera le message temporairement avant la redirection
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        DateTime Min = DateTime.Now.AddYears(-18);
        if (ModelState.IsValid)
        {
            if (model.Date >= Min)
            {
                ModelState.AddModelError("", "Le médecin doit ètre majeur");
            }
            else
            {
                var medecin = new Medecin
                {
                    UserName = model.UserName,
                    Role = model.Role,
                    Date_naissance_m = model.Date,
                };

                var result = await _userManager.CreateAsync(medecin, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(medecin, isPersistent: false);
                    return RedirectToAction("Index", "Dashboard");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(string id, string UserName, string Role, DateTime Date_naissance_m, string PasswordHash)
    {
        DateTime Min = DateTime.Now.AddYears(-18);
        Medecin user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            if (!string.IsNullOrEmpty(UserName))
                user.UserName = UserName;
            else
                ModelState.AddModelError("", "UserName ne peut pas être vide");
            if (!string.IsNullOrEmpty(Role))
                user.Role = Role;
            else
                ModelState.AddModelError("", "Role ne peut pas être vide");
            if (Date_naissance_m < Min)
                user.Date_naissance_m = Date_naissance_m;
            else
                ModelState.AddModelError("", "Le médecin a minumun 18 ans");

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (!string.IsNullOrEmpty(PasswordHash))
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, PasswordHash);
                if (!passwordChangeResult.Succeeded)
                    foreach (var error in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

        }
        return View(user);
    }

    // Edit: MedecinController 
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Medecin user = _userManager.FindByIdAsync(userId).Result;


        return View(user);
    }

     public IActionResult ContactNonConnecter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactNonConnecter(MailViewModel viewModel)

        {
            string sender = "bts.testperso@gmail.com";
            string pw = "ppig gflv vmwp ctve";
            string receiver = "esteallier@gmail.com";
            string messageComplet = viewModel.Nom + "\n" + viewModel.Prenom + "\n" + viewModel.email
            + "\n" + viewModel.telephone + "\n" + viewModel.message;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(sender);
            message.Subject = "Nouveau Message";
            message.To.Add(receiver);
            message.Body = messageComplet;
            if (!ModelState.IsValid)
                return View(viewModel);

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(sender, pw),
                EnableSsl = true,
            };
            smtpClient.Send(message);
            return RedirectToAction("Login", "Account");
        }


}
