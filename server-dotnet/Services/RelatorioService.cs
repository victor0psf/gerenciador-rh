using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using server_dotnet.Models;

public class RelatorioService : IRelatorioService
{
    public byte[] GerarRelatorioFuncionariosPdf(List<Funcionario> funcionarios)
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

                        static IContainer CellStyle(IContainer container) =>
                            container.PaddingVertical(5)
                                     .BorderBottom(1)
                                     .BorderColor(Colors.Grey.Lighten2);
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
