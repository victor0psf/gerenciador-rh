using server_dotnet.Models;

public class HistoricoAlteracao
{
    public int Id { get; set; }
    public DateTime DataHoraAlteracao { get; set; }
    public string CampoAlterado { get; set; } = "";
    public string ValorAntigo { get; set; } = "";
    public string ValorNovo { get; set; } = "";
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }

    public HistoricoAlteracao() { }

    public HistoricoAlteracao(string campo, string valorAntigo, string valorNovo, DateTime data, int funcionarioId)
    {
        CampoAlterado = campo;
        ValorAntigo = valorAntigo;
        ValorNovo = valorNovo;
        DataHoraAlteracao = data;
        FuncionarioId = funcionarioId;
    }
}
