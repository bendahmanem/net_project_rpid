using ASPBookProject.Data;
using ASPBookProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPBookProject.Controllers
{
    public class StatistiqueController : Controller
    {
        private readonly UserManager<Medecin> _userManager;
        private readonly ApplicationDbContext _context;
        public StatistiqueController(UserManager<Medecin> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            // Réinitialiser tous les compteurs à zéro
            var medicaments = await _context.Medicaments.ToListAsync();
            foreach (var medicament in medicaments)
            {
                medicament.compteur = 0;
            }

            // Recalculer les compteurs en fonction des ordonnances existantes
            var ordonnances = await _context.Ordonnances
                .Include(o => o.Medicaments)
                .ToListAsync();

            foreach (var ordonnance in ordonnances)
            {
                foreach (var medicament in ordonnance.Medicaments)
                {
                    var med = medicaments.FirstOrDefault(m => m.MedicamentId == medicament.MedicamentId);
                    if (med != null)
                    {
                        med.compteur++;
                    }
                }
            }
            var dateNow = DateTime.Now.Date;
            var ordonnancesEnCours = await _context.Ordonnances
                .Include(o => o.Patient)
                .Include(o => o.Medecin)
                .Include(o => o.Medicaments)
                .Where(o => o.Date_debut <= dateNow && o.Date_fin >= dateNow)
                .OrderBy(o => o.Date_fin)
                .ToListAsync();

            await _context.SaveChangesAsync();

            ViewBag.OrdonnancesEnCours = ordonnancesEnCours;
            return View(medicaments.OrderByDescending(m => m.compteur));

        }


    }
}
