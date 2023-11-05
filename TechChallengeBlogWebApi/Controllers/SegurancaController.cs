using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using TechChallengeBlogWebApi.Interfaces;
using TechChallengeBlogWebApi.Models.Response;

#nullable disable

namespace TechChallengeBlogWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SegurancaController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public SegurancaController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("token/{chave}")]
        [SwaggerOperation(Summary = "Endpoint para gerar o token JWT validando uma chave de segurança.",
                      Description = "Endpoint para gerar o token JWT validando uma chave de segurança.")]
        [SwaggerResponse(200, "OK", typeof(TokenResponseModel))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "BadRequest", typeof(ErrorResponseModel))]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> GetAsync([FromRoute] string chave)
        {
            try
            {
                string token = await _tokenService.GerarTokenAsync(chave);

                return string.IsNullOrEmpty(token) ? NoContent() : Ok(new TokenResponseModel
                {
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Erro = ex.Message
                });
            }
        }
    }
}