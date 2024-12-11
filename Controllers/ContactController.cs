using System.Net;
using System.Net.Mail;
using ASPBookProject.ViewModels;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPBookProject.Controllers
{
    public class ContactController : Controller
    {
        [Authorize]
        public IActionResult Faq()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Contact()
        {
            return View("Contact", "Dashboard");
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
