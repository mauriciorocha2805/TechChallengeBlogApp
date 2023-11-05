using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TechChallengeBlogWebApp.Models;
using TechChallengeBlogWebApp.Services;

#nullable disable

namespace TechChallengeBlogWebApp.Pages
{
    public class EditarModel : PageModel
    {
        private readonly BlogService _service;

        public EditarModel(BlogService service)
        {
            _service = service;
        }

        [BindProperty]
        public NoticiaModel Noticia { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Noticia = await _service.PesquisarNoticiaPorIdAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await _service.AtualizarAsync(Noticia);

            return RedirectToPage("/Index");
        }
    }
}