using server_dotnet.Models;


public interface IRelatorioService
{
    byte[] GerarRelatorioFuncionariosPdf(List<Funcionario> funcionarios);
}
