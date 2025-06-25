using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarefasApp.Models;


namespace TarefasApp.Pages
{
    public class ExcluirModel : PageModel
    {
        private readonly AppDbContext _context;

        public ExcluirModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tarefa Tarefa { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tarefa = await _context.Tarefas.FindAsync(id);

            if (Tarefa == null) return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var tarefa = await _context.Tarefas.FindAsync(Tarefa.Id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
