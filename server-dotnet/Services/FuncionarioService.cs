using Microsoft.EntityFrameworkCore;
using server_dotnet.Models;
using server_dotnet.Data;
using server_dotnet.DTOs;

namespace server_dotnet.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly AppDbContext _appDbContext;

        public FuncionarioService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> EditarFuncionarioAsync(int id, FuncionariosUpdateDTO updateFuncionario)
        {
            var funcionario = await _appDbContext.Funcionarios
                .Include(f => f.HistoricosAlteracao)
                .Include(f => f.Salarios)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
                return false;

            var historicos = new List<HistoricoAlteracao>();
            var dataHora = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(updateFuncionario.Nome) && funcionario.Nome != updateFuncionario.Nome)
            {
                historicos.Add(new("Nome", funcionario.Nome, updateFuncionario.Nome, dataHora, funcionario.Id));
                funcionario.Nome = updateFuncionario.Nome;
            }

            if (!string.IsNullOrWhiteSpace(updateFuncionario.Cargo) && funcionario.Cargo != updateFuncionario.Cargo)
            {
                historicos.Add(new("Cargo", funcionario.Cargo, updateFuncionario.Cargo, dataHora, funcionario.Id));
                funcionario.Cargo = updateFuncionario.Cargo;
            }

            if (updateFuncionario.DataAdmissao.HasValue && funcionario.DataAdmissao != updateFuncionario.DataAdmissao.Value)
            {
                historicos.Add(new("DataAdmissao", funcionario.DataAdmissao.ToString("s"), updateFuncionario.DataAdmissao.Value.ToString("s"), dataHora, funcionario.Id));
                funcionario.DataAdmissao = updateFuncionario.DataAdmissao.Value;
            }

            if (updateFuncionario.Status.HasValue && funcionario.Status != updateFuncionario.Status.Value)
            {
                historicos.Add(new("Status", funcionario.Status.ToString(), updateFuncionario.Status.Value.ToString(), dataHora, funcionario.Id));
                funcionario.Status = updateFuncionario.Status.Value;
            }

            var salarioAtual = funcionario.Salarios?.FirstOrDefault();
            var novoSalarioDTO = updateFuncionario.Salarios?.FirstOrDefault();

            if (novoSalarioDTO != null && novoSalarioDTO.Salario > 0)
            {
                if (salarioAtual != null)
                {
                    if (salarioAtual.Salario != novoSalarioDTO.Salario)
                    {
                        historicos.Add(new("Salario", salarioAtual.Salario.ToString("F2"), novoSalarioDTO.Salario.ToString("F2"), dataHora, funcionario.Id));
                        salarioAtual.Salario = novoSalarioDTO.Salario;
                    }

                    if (novoSalarioDTO.SalarioMedio > 0 && salarioAtual.SalarioMedio != novoSalarioDTO.SalarioMedio)
                    {
                        historicos.Add(new("SalarioMedio", salarioAtual.SalarioMedio.ToString("F2"), novoSalarioDTO.SalarioMedio.ToString("F2"), dataHora, funcionario.Id));
                        salarioAtual.SalarioMedio = novoSalarioDTO.SalarioMedio;
                    }
                }
                else
                {
                    funcionario.Salarios = new List<SalarioFuncionario>
                {
                    new() {
                        Salario = novoSalarioDTO.Salario,
                        SalarioMedio = novoSalarioDTO.SalarioMedio
                    }
                };

                    historicos.Add(new("Salario", "Nenhum", novoSalarioDTO.Salario.ToString("F2"), dataHora, funcionario.Id));
                }
            }

            _appDbContext.HistoricoAlteracoes.AddRange(historicos);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}