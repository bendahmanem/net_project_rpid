using ASPBookProject.Data;
using ASPBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Net.Mail;
using ASPBookProject.ViewModels;
using AspNetCoreGeneratedDocument;

namespace ASPBookProject.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Index
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Contact(MailViewModel viewModel)

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
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
