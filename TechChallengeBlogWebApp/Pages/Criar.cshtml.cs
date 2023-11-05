using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TechChallengeBlogWebApp.Models;
using TechChallengeBlogWebApp.Services;

#nullable disable

namespace TechChallengeBlogWebApp.Pages
{
    public class CriarModel : PageModel
    {
        private readonly BlogService _service;

        public CriarModel(BlogService service)
        {
            _service = service;
        }

        [BindProperty]
        public NoticiaModel Noticia { get; set; }

        public async Task<IActionResult> OnPost()
        {
            await _service.IncluirAsync(Noticia);

            return RedirectToPage("/Index");
        }

        public void OnGet()
        {

        }
    }
}