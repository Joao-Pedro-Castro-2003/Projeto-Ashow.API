using Ashow.Business.Interfaces;
using Ashow.Data.Repository;
using Ashow.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ashow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioModel usuario)
        {
            try
            {
                await _usuarioService.Cadastrar(usuario);
                return Ok("Usuário cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public async Task<IActionResult> Deletar([FromRoute] int Id)
        {
            try
            {
                await _usuarioService.Deletar(Id);
                return Ok("Usuário deleteado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] UsuarioModel usuario)
        {
            try
            {
                await _usuarioService.Atualizar(usuario);
                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId([FromRoute] int Id)
        {
            try
            {
                var usuario = await _usuarioService.BuscarPorId(Id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
