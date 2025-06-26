using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarefasApp.Models;

namespace TarefasApp.Pages
{
    public class EditarModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditarModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tarefa Tarefa { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tarefa = await _context.Tarefas.FindAsync(id);
            if (Tarefa == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var tarefaExistente = await _context.Tarefas.FindAsync(Tarefa.Id);
            if (tarefaExistente == null)
            {
                return RedirectToPage("/Index");
            }

            tarefaExistente.Titulo = Tarefa.Titulo;
            tarefaExistente.Descricao = Tarefa.Descricao;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
