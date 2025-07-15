using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using server_dotnet.Data;
using server_dotnet.DTOs;
using server_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

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

            // Configura a licença community do QuestPDF para evitar o erro
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }

        // Adiciona um novo funcionário com salários
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

        // Lista todos os funcionários (sem salários)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            try
            {
                var funcionarios = await _appDbContext.Funcionarios.ToListAsync();
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

        // Busca funcionário por ID com salários
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

        // Lista funcionários por cargo
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

        // Edita funcionário e registra histórico de alterações
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFuncionario(int id, [FromBody] FuncionariosUpdateDTO updateFuncionario)
        {
            var funcionario = await _appDbContext.Funcionarios
                .Include(f => f.HistoricosAlteracao)
                .Include(f => f.Salarios)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
                return NotFound();

            var historicos = new List<HistoricoAlteracao>();
            var dataHora = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(updateFuncionario.Nome) && funcionario.Nome != updateFuncionario.Nome)
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

            if (!string.IsNullOrWhiteSpace(updateFuncionario.Cargo) && funcionario.Cargo != updateFuncionario.Cargo)
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

            if (updateFuncionario.DataAdmissao.HasValue &&
                funcionario.DataAdmissao != updateFuncionario.DataAdmissao.Value)
            {
                historicos.Add(new HistoricoAlteracao
                {
                    DataHoraAlteracao = dataHora,
                    CampoAlterado = "DataAdmissao",
                    ValorAntigo = funcionario.DataAdmissao.ToString("s"),
                    ValorNovo = updateFuncionario.DataAdmissao.Value.ToString("s"),
                    FuncionarioId = funcionario.Id
                });
                funcionario.DataAdmissao = updateFuncionario.DataAdmissao.Value;
            }

            if (updateFuncionario.Status.HasValue && funcionario.Status != updateFuncionario.Status.Value)
            {
                historicos.Add(new HistoricoAlteracao
                {
                    DataHoraAlteracao = dataHora,
                    CampoAlterado = "Status",
                    ValorAntigo = funcionario.Status.ToString(),
                    ValorNovo = updateFuncionario.Status.Value.ToString(),
                    FuncionarioId = funcionario.Id
                });
                funcionario.Status = updateFuncionario.Status.Value;
            }

            var salarioAtual = funcionario.Salarios?.FirstOrDefault();
            var novoSalarioDTO = updateFuncionario.Salarios?.FirstOrDefault();

            if (salarioAtual != null && novoSalarioDTO != null)
            {
                if (novoSalarioDTO.Salario > 0 && salarioAtual.Salario != novoSalarioDTO.Salario)
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

                if (novoSalarioDTO.SalarioMedio > 0 && salarioAtual.SalarioMedio != novoSalarioDTO.SalarioMedio)
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

        // Retorna média salarial geral
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

        // Marca funcionário como desligado
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

        // Gera relatório PDF usando QuestPDF
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

                var pdfBytes = GerarPdfFuncionarios(funcionarios);

                return File(pdfBytes, "application/pdf", "RelatorioFuncionarios.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar relatório PDF: " + ex.Message);
                return StatusCode(500, $"Erro interno ao gerar relatório: {ex.Message}");
            }
        }

        private byte[] GerarPdfFuncionarios(List<Funcionario> funcionarios)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Relatório de Funcionários")
                        .SemiBold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Nome");
                                header.Cell().Element(CellStyle).Text("Cargo");
                                header.Cell().Element(CellStyle).Text("Data Admissão");
                                header.Cell().Element(CellStyle).Text("Salário");
                                header.Cell().Element(CellStyle).Text("Status");
                            });

                            foreach (var f in funcionarios)
                            {
                                table.Cell().Element(CellStyle).Text(f.Nome ?? "-");
                                table.Cell().Element(CellStyle).Text(f.Cargo ?? "-");
                                table.Cell().Element(CellStyle).Text(f.DataAdmissao.ToString("dd/MM/yyyy"));
                                var salario = f.Salarios?.FirstOrDefault()?.Salario ?? 0;
                                table.Cell().Element(CellStyle).Text($"R$ {salario:F2}");
                                table.Cell().Element(CellStyle).Text(f.Status ? "Ativo" : "Inativo");
                            }

                            IContainer CellStyle(IContainer container)
                            {
                                return container.PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Página ");
                            text.CurrentPageNumber();
                            text.Span(" de ");
                            text.TotalPages();
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
