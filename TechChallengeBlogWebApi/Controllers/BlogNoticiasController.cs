using App.Blog.Application.Interfaces;
using App.Blog.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechChallengeBlogWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNoticiasController : ControllerBase
    {
        private readonly INoticiaService _service;

        public BlogNoticiasController(INoticiaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation(Summary = "Endpoint para obter todas a notícias presentes na base de dados.", Description = "Endpoint para obter todas a notícias presentes na base de dados.")]
        [SwaggerResponse(200, "OK", typeof(List<Noticia>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> GetAsync()
        {
            List<Noticia> noticias = await _service.ConsultarAsync();
            return noticias == null || noticias.Count.Equals(0) ? NoContent() : Ok(noticias);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Endpoint para obter a noticia por Id presente na base de dados.", Description = "Endpoint para obter a noticia por Id presente na base de dados.")]
        [SwaggerResponse(200, "OK", typeof(Noticia))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> GetPorIdAsync(int id)
        {
            Noticia noticia = await _service.ConsultarPorIdAsync(id);
            return noticia == null ? NoContent() : Ok(noticia);
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(Summary = "Endpoint para incluir notícias na base de dados.", Description = "Endpoint para incluir notícias na base de dados.")]
        [SwaggerResponse(200, "OK", typeof(Noticia))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> PostAsync(Noticia noticia)
        {
            int retorno = await _service.IncluirAsync(noticia);
            return retorno.Equals(0) ? NoContent() : Ok(noticia);
        }

        [HttpPut]
        [Route("")]
        [SwaggerOperation(Summary = "Endpoint para atualizar notícias na base de dados.", Description = "Endpoint para incluir notícias na base de dados.")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> AtualizarAsync(Noticia noticia)
        {
            int retorno = await _service.AtualizarAsync(noticia);
            return retorno.Equals(0) ? NoContent() : Ok();
        }
    }
}