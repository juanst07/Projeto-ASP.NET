using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TarefasApp.Models;

namespace TarefasApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tarefa NovaTarefa { get; set; }

        public List<Tarefa> Tarefas { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Tarefas = await _context.Tarefas.OrderBy(t => t.Concluida).ThenBy(t => t.Id).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            NovaTarefa.Concluida = false;
            _context.Tarefas.Add(NovaTarefa);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleConcluidaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                tarefa.Concluida = !tarefa.Concluida;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
