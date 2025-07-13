using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_dotnet.Data;
using server_dotnet.DTOs;
using server_dotnet.Models;

namespace server_dotnet.Controllers
{
    [Route("api/[controller]")]
    public class FeriasController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<FeriasController> _logger;

        public FeriasController(AppDbContext appDbContext, ILogger<FeriasController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todas as férias cadastradas, incluindo os funcionários relacionados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ferias>>> GetFeriasAll()
        {
            try
            {
                var allferias = await _appDbContext.Ferias.Include(f => f.Funcionario).ToListAsync();
                if (allferias == null)
                {
                    return NotFound("Não foi possível encontrar!");
                }
                return Ok(allferias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar exibir Ferias!");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca uma férias específica pelo ID, incluindo o funcionário relacionado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Ferias>> GetFeriasById(int id)
        {
            try
            {
                var ferias = await _appDbContext.Ferias.Include(f => f.Funcionario)
                .FirstOrDefaultAsync(f => f.Id == id);
                if (ferias == null)
                {
                    return NotFound("Não foi possível encontrar!");
                }
                return ferias;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao exibir as férias do Funcionário");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Adiciona um novo registro de férias para um funcionário
        /// </summary>
        /// <param name="feriasDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFerias([FromBody] FeriasDTO feriasDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var ferias = new Ferias
                {
                    DataInicio = feriasDTO.DataInicio,
                    DataTermino = feriasDTO.DataTermino,
                    FuncionarioId = feriasDTO.FuncionarioId
                };
                _appDbContext.Ferias.Add(ferias);
                await _appDbContext.SaveChangesAsync();

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao cadastrar");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza os dados de um registro de férias pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateFerias"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFerias(int id, [FromBody] FeriasDTO updateFerias)
        {
            try
            {
                var ferias = await _appDbContext.Ferias.FindAsync(id);
                if (ferias == null)
                {
                    return NotFound();
                }

                ferias.DataInicio = updateFerias.DataInicio;
                ferias.DataTermino = updateFerias.DataTermino;
                ferias.FuncionarioId = updateFerias.FuncionarioId;

                await _appDbContext.SaveChangesAsync();

                return Ok("Ferias Atualizadas com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao atualizar as férias");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deleta um registro de férias pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFerias(int id)
        {
            try
            {
                var ferias = await _appDbContext.Ferias.FindAsync(id);
                if (ferias == null)
                    return NotFound("Não foi possível encontrar!");

                _appDbContext.Ferias.Remove(ferias);
                await _appDbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao Deletar as férias");
                return StatusCode(500);
            }
        }
    }
}