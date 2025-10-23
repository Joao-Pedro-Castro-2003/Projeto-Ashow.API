using Ashow.Business;
using Microsoft.AspNetCore.Mvc;
using Ashow.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Ashow.Business.Interfaces;

namespace Ashow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AshowController : ControllerBase
    {
        private readonly IAshowService _ashowService;

        public AshowController(IAshowService ashowService)
        {
            _ashowService = ashowService;
        }

        [Authorize]
        [HttpGet]
        [Route("BuscaFilmesPorNome")]
        public async Task<IActionResult> GetFilmesPorNomeAsync([FromQuery] string nome)
        {
            try
            {
                var filmes = await _ashowService.GetFilmesPorNomeAsync(nome);
                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("BuscaFilmesPorGenero")]
        public async Task<IActionResult> GetFilmesPorGeneroAsync([FromQuery] int generoId)
        {
            try
            {
                var filmes = await _ashowService.GetFilmesPorGeneroAsync(generoId);
                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("BuscaFilmes")]
        public async Task<IActionResult> GetFilmesAsync()
        {
            try
            {
                var filmes = await _ashowService.GetFilmesAsync();
                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("BuscaSeries")]
        public async Task<IActionResult> GetSeriesAsync()
        {
            try
            {
                var series = await _ashowService.GetSeriesAsync();
                return Ok(series);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("BuscaGeneros")]
        public async Task<IActionResult> GetGenerosAsync()
        {
            try
            {
                var generos = await _ashowService.GetGenerosAsync();
                return Ok(generos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("InserirFilme")]
        public async Task<IActionResult> InserirFilmeAsync([FromBody] FilmeModel filme)
        {
            try
            {
                await _ashowService.InserirFilmeAsync(filme);
                var filmes = await _ashowService.GetFilmesAsync();
                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [Authorize]
        [HttpPost]
        [Route("InserirSerie")]
        public async Task<IActionResult> InserirSerieAsync([FromBody] SerieModel serie)
        {
            try
            {
                await _ashowService.InserirSerieAsync(serie);
                var series = await _ashowService.GetSeriesAsync();
                return Ok(series);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeletarFilme")]
        public async Task<IActionResult> DeletarFilmeAsyncAsync([FromQuery] int filmeId)
        {
            try
            {
                await _ashowService.DeletarFilmeAsync(filmeId);
                var filmes = await _ashowService.GetFilmesAsync();
                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeletarSerie")]
        public async Task<IActionResult> DeletarSerieAsyncAsync([FromQuery] int serieId)
        {
            try
            {
                await _ashowService.DeletarSerieAsync(serieId);
                var series = await _ashowService.GetSeriesAsync();
                return Ok(series);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [Authorize]
        [HttpPut]
        [Route("AtualizarFilme")]
        public async Task<IActionResult> AtualizarFilmeAsync([FromBody] FilmeModel filme)
        {
            try
            {
                await _ashowService.AtualizarFilmeAsync(filme);
                var retorno = await _ashowService.GetFilmesAsync();
                return Ok(retorno);
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [Authorize]
        [HttpPut]
        [Route("AtualizarSerie")]
        public async Task<IActionResult> AtualizarSerieAsync([FromBody] SerieModel serie)
        {
            try
            {
                await _ashowService.AtualizarSerieAsync(serie);
                var retorno = await _ashowService.GetSeriesAsync();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
