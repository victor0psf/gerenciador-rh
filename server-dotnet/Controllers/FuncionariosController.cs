using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using server_dotnet.Data;
using server_dotnet.DTOs;
using server_dotnet.Models;
using server_dotnet.Services;

namespace server_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<FuncionariosController> _logger;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IRelatorioService _relatorioService;

        public FuncionariosController(AppDbContext appDbContext, ILogger<FuncionariosController> logger, IFuncionarioService funcionarioService, IRelatorioService relatorioService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _funcionarioService = funcionarioService;
            _relatorioService = relatorioService;

            // Configura a licença community do QuestPDF
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }

        /// <summary>
        /// Adiciona um novo funcionário com salários
        /// </summary>
        /// <param name="funcionariosDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFuncionario([FromBody] FuncionariosDTO funcionariosDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var funcionario = new Funcionario
                {
                    Nome = funcionariosDTO.Nome,
                    Cargo = funcionariosDTO.Cargo,
                    DataAdmissao = funcionariosDTO.DataAdmissao,
                    Status = funcionariosDTO.Status,
                    Salarios = funcionariosDTO.Salarios?.Select(s => new SalarioFuncionario
                    {
                        Salario = s.Salario,
                        SalarioMedio = s.SalarioMedio
                    }).ToList()
                };
                _appDbContext.Funcionarios.Add(funcionario);
                await _appDbContext.SaveChangesAsync();

                return Created("", funcionario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Não foi possível cadastrar o novo funcionário!");
                return StatusCode(500, "Erro interno ao cadastrar funcionário.");
            }
        }

        /// <summary>
        /// Lista todos os funcionários (sem salários)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            try
            {
                var funcionarios = await _appDbContext.Funcionarios
                .Include(f => f.Salarios)
                .ToListAsync();

                if (!funcionarios.Any())
                    return NotFound("Nenhum funcionário encontrado.");

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar funcionários.");
                return StatusCode(500, "Erro interno ao listar funcionários.");
            }
        }

        /// <summary>
        /// Busca funcionário por ID com salários
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionarioById(int id)
        {
            try
            {
                var funcionario = await _appDbContext.Funcionarios
                    .Include(f => f.Salarios)
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (funcionario == null)
                    return NotFound();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar funcionário por ID.");
                return StatusCode(500, "Erro interno ao buscar funcionário.");
            }
        }

        /// <summary>
        /// Lista funcionários por cargo
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns></returns>
        [HttpGet("cargo/{cargo}")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionariosCargo(string cargo)
        {
            try
            {
                var funcionarios = await _appDbContext.Funcionarios
                    .Where(f => f.Cargo.ToLower() == cargo.ToLower())
                    .ToListAsync();

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao listar funcionários com cargo {cargo}.");
                return StatusCode(500, "Erro interno ao listar funcionários por cargo.");
            }
        }

        /// <summary>
        /// Edita funcionário e registra histórico de alterações
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateFuncionario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFuncionario(int id, [FromBody] FuncionariosUpdateDTO updateFuncionario)
        {
            try
            {
                var editar = await _funcionarioService.EditarFuncionarioAsync(id, updateFuncionario);
                if (!editar)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao Editar!");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna média salarial geral
        /// </summary>
        /// <returns></returns>
        [HttpGet("media-salarial")]
        public async Task<ActionResult<decimal>> GetSalarioMediaAll()
        {
            try
            {
                var salarios = await _appDbContext.SalarioFuncionarios.ToListAsync();

                if (salarios == null || !salarios.Any())
                    return NotFound("Nenhum salário encontrado.");

                var media = salarios.Average(s => s.Salario);

                return Ok(media);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao calcular salário médio.");
                return StatusCode(500, "Erro interno ao calcular salário médio.");
            }
        }

        /// <summary>
        /// Marca funcionário como desligado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> DesligarFuncionario(int id)
        {
            var funcionario = await _appDbContext.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return NotFound($"Funcionário com ID {id} não encontrado.");

            funcionario.Status = false;
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gera relatório PDF usando QuestPDF
        /// </summary>
        /// <returns></returns>
        [HttpGet("relatorio-pdf")]
        public IActionResult GerarRelatorioFuncionariosPDF()
        {
            try
            {
                var funcionarios = _appDbContext.Funcionarios
                    .Include(f => f.Salarios)
                    .OrderBy(f => f.Id)
                    .ToList();

                if (funcionarios == null || !funcionarios.Any())
                {
                    _logger.LogWarning("Tentativa de gerar PDF sem funcionários cadastrados.");
                    return NotFound("Nenhum funcionário encontrado.");
                }

                var pdfBytes = _relatorioService.GerarRelatorioFuncionariosPdf(funcionarios);

                return File(pdfBytes, "application/pdf", "RelatorioFuncionarios.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar relatório PDF: " + ex.Message);
                return StatusCode(500, $"Erro interno ao gerar relatório: {ex.Message}");
            }
        }

        [HttpGet("webforms")]
        public async Task<ActionResult<IEnumerable<object>>> GetFuncionariosWebForms()
        {
            var funcionarios = await _appDbContext.Funcionarios
                .Include(f => f.Salarios)
                .Select(f => new
                {
                    f.Id,
                    f.Nome,
                    f.Cargo,
                    f.DataAdmissao,
                    f.Status,
                    SalarioAtual = f.Salarios
                        .OrderByDescending(s => s.Id)
                        .Select(s => (decimal?)s.Salario)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(funcionarios);
        }


    }
}
