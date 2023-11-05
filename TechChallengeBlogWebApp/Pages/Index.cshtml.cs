using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechChallengeBlogWebApp.Models;
using TechChallengeBlogWebApp.Services;

#nullable disable

namespace TechChallengeBlogWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BlogService _service;

        public IndexModel(BlogService service)
        {
            _service = service;
        }

        public List<NoticiaModel> Noticias { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Noticias = await _service.PesquisarTodasNoticiasAsync();

            return Page();
        }
    }
}