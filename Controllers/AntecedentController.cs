using ASPBookProject.Data;
using ASPBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPBookProject.Controllers
{
    public class AntecedentController : Controller
    {
        // GET: AntecedentController
        private readonly ApplicationDbContext _context;
        public AntecedentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Antecedent> antecedents = new List<Antecedent>();
            antecedents = await _context.Antecedents.ToListAsync();
            return View(antecedents);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var antecedent = await _context.Antecedents.FirstOrDefaultAsync(a => a.AntecedentId == id);
            if (antecedent == null)
                return NotFound();

            return View(antecedent);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var antecedent = await _context.Antecedents.FirstOrDefaultAsync(a => a.AntecedentId == id);
            if (antecedent == null)
                return NotFound();


            _context.Antecedents.Remove(antecedent);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var antecedent = await _context.Antecedents.FirstOrDefaultAsync(a => a.AntecedentId == id);
            if (antecedent == null)
                return NotFound();

            return View(antecedent);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Antecedent antecedent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Antecedent? antec = _context.Antecedents.FirstOrDefault<Antecedent>(ant => ant.AntecedentId == antecedent.AntecedentId);
            if (antec != null)
            {
                antec.Libelle_a = antecedent.Libelle_a;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();

        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(Antecedent antecedent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Antecedents.Add(antecedent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
