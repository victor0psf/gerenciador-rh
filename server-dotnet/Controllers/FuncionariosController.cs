using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_dotnet.Data;
using server_dotnet.DTOs;
using server_dotnet.Models;
using iText.Layout;

namespace server_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<FuncionariosController> _logger;

        public FuncionariosController(AppDbContext appDbContext, ILogger<FuncionariosController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        /// <summary>
        /// Adiciona um novo funcionário ao banco de dados
        /// Recebe um DTO com os dados do funcionário e seus salários.
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

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Não foi possivel cadastrar o novo funcionário!");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna a lista completa de funcionários cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            try
            {
                var funcionarios = await _appDbContext.Funcionarios.ToListAsync();
                if (!funcionarios.Any())
                {
                    return NotFound("Nenhum funcionário encontrado.");
                }
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar exibir a lista de funcionários");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca um funcionário pelo seu ID, incluindo os salários
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionarioById(int id)
        {
            try
            {
                var funcionario = await _appDbContext.Funcionarios.Include(f => f.Salarios)
                .FirstOrDefaultAsync(f => f.Id == id);

                if (funcionario == null)
                {
                    return NotFound();
                }
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Não foi possivel encontrar o funcionario");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna a lista de funcionários filtrada por cargo
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns></returns>
        [HttpGet("cargo/{cargo}")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionariosCargo(string cargo)
        {
            try
            {
                var funcionarios = await _appDbContext.Funcionarios.Where
                (f => f.Cargo.ToLower() == cargo.ToLower())
                .ToListAsync();

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Nenhum funcionário encontrado com o cargo: {cargo}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Edita os dados de um funcionário, registrando as alterações no histórico.
        /// Atualiza informações básicas e os dados salariais.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateFuncionario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFuncionario(int id, [FromBody] FuncionariosDTO updateFuncionario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var funcionario = await _appDbContext.Funcionarios
                .Include(f => f.HistoricosAlteracao)
                .Include(f => f.Salarios)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null) return NotFound();

            var historicos = new List<HistoricoAlteracao>();
            var dataHora = DateTime.Now;

            if (funcionario.Nome != updateFuncionario.Nome)
            {
                historicos.Add(new HistoricoAlteracao
                {
                    DataHoraAlteracao = dataHora,
                    CampoAlterado = "Nome",
                    ValorAntigo = funcionario.Nome,
                    ValorNovo = updateFuncionario.Nome,
                    FuncionarioId = funcionario.Id
                });
                funcionario.Nome = updateFuncionario.Nome;
            }

            if (funcionario.Cargo != updateFuncionario.Cargo)
            {
                historicos.Add(new HistoricoAlteracao
                {
                    DataHoraAlteracao = dataHora,
                    CampoAlterado = "Cargo",
                    ValorAntigo = funcionario.Cargo,
                    ValorNovo = updateFuncionario.Cargo,
                    FuncionarioId = funcionario.Id
                });
                funcionario.Cargo = updateFuncionario.Cargo;
            }

            if (funcionario.DataAdmissao != updateFuncionario.DataAdmissao)
            {
                historicos.Add(new HistoricoAlteracao
                {
                    DataHoraAlteracao = dataHora,
                    CampoAlterado = "DataAdmissao",
                    ValorAntigo = funcionario.DataAdmissao.ToString("s"),
                    ValorNovo = updateFuncionario.DataAdmissao.ToString("s"),
                    FuncionarioId = funcionario.Id
                });
                funcionario.DataAdmissao = updateFuncionario.DataAdmissao;
            }

            if (funcionario.Status != updateFuncionario.Status)
            {
                historicos.Add(new HistoricoAlteracao
                {
                    DataHoraAlteracao = dataHora,
                    CampoAlterado = "Status",
                    ValorAntigo = funcionario.Status.ToString(),
                    ValorNovo = updateFuncionario.Status.ToString(),
                    FuncionarioId = funcionario.Id
                });
                funcionario.Status = updateFuncionario.Status;
            }

            var salarioAtual = funcionario.Salarios?.FirstOrDefault();
            var novoSalarioDTO = updateFuncionario.Salarios?.FirstOrDefault();

            if (salarioAtual != null && novoSalarioDTO != null)
            {
                if (salarioAtual.Salario != novoSalarioDTO.Salario)
                {
                    historicos.Add(new HistoricoAlteracao
                    {
                        DataHoraAlteracao = dataHora,
                        CampoAlterado = "Salario",
                        ValorAntigo = salarioAtual.Salario.ToString("F2"),
                        ValorNovo = novoSalarioDTO.Salario.ToString("F2"),
                        FuncionarioId = funcionario.Id
                    });

                    salarioAtual.Salario = novoSalarioDTO.Salario;
                }

                if (salarioAtual.SalarioMedio != novoSalarioDTO.SalarioMedio)
                {
                    historicos.Add(new HistoricoAlteracao
                    {
                        DataHoraAlteracao = dataHora,
                        CampoAlterado = "SalarioMedio",
                        ValorAntigo = salarioAtual.SalarioMedio.ToString("F2"),
                        ValorNovo = novoSalarioDTO.SalarioMedio.ToString("F2"),
                        FuncionarioId = funcionario.Id
                    });

                    salarioAtual.SalarioMedio = novoSalarioDTO.SalarioMedio;
                }
            }

            _appDbContext.HistoricoAlteracoes.AddRange(historicos);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Calcula e retorna a média salarial geral de todos os funcionários
        /// </summary>
        /// <returns></returns>
        [HttpGet("media-salarial")]
        public async Task<ActionResult<Decimal>> GetSalarioMediaAll()
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
                _logger.LogError(ex, "Erro ao calcular o salário médio geral.");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Marca o funcionário como desligado (Status = false)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> DesligarFuncionario(int id)
        {
            var funcionario = await _appDbContext.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound($"Funcionário com ID {id} não encontrado.");
            }
            funcionario.Status = false;
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gera um relatório PDF com todos os funcionários cadastrados,
        /// incluindo nome, cargo, data de admissão, salário e status.
        /// </summary>
        /// <returns></returns>
        [HttpGet("relatorio-pdf")]
        public async Task<IActionResult> GerarRelatorioFuncionariosPDF()
        {
            try
            {
                var funcionarios = await _appDbContext.Funcionarios
                    .Include(f => f.Salarios)
                    .ToListAsync();

                if (!funcionarios.Any())
                    return NotFound("Nenhum funcionário encontrado.");

                var pdfBytes = GerarPdfFuncionarios(funcionarios);

                return File(pdfBytes, "application/pdf", "RelatorioFuncionarios.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar relatório de funcionários.");
                return StatusCode(500, "Erro ao gerar o relatório.");
            }
        }

        // Método privado que gera o PDF e retorna o conteúdo em bytes
        private byte[] GerarPdfFuncionarios(List<Funcionario> funcionarios)
        {
            using var memoryStream = new MemoryStream();
            using var pdfWriter = new PdfWriter(memoryStream);
            using var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            // Título
            var titulo = new Paragraph("Relatório de Funcionários")
                .SetFontSize(18)
                .SetTextAlignment(TextAlignment.CENTER);
            document.Add(titulo);

            document.Add(new Paragraph($"Data: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n"));

            // Tabela
            var tabela = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();
            tabela.AddHeaderCell("Nome");
            tabela.AddHeaderCell("Cargo");
            tabela.AddHeaderCell("Data Admissão");
            tabela.AddHeaderCell("Salário");
            tabela.AddHeaderCell("Status");

            foreach (var f in funcionarios)
            {
                var salario = f.Salarios.FirstOrDefault()?.Salario ?? 0;

                tabela.AddCell(f.Nome);
                tabela.AddCell(f.Cargo);
                tabela.AddCell(f.DataAdmissao.ToString("dd/MM/yyyy"));
                tabela.AddCell($"R$ {salario:F2}");
                tabela.AddCell(f.Status ? "Ativo" : "Inativo");
            }

            document.Add(tabela);
            document.Close();

            return memoryStream.ToArray();
        }
    }
}