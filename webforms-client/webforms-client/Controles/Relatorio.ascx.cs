using System;
using System.Net.Http;
using System.Web.UI;

namespace webforms_client.Controles
{
    public partial class Relatorio : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/");
                var response = client.GetAsync("api/funcionarios/relatorio-pdf").Result;

                if (response.IsSuccessStatusCode)
                {
                    var pdfBytes = response.Content.ReadAsByteArrayAsync().Result;

                    // Prepara a resposta HTTP para forçar o download
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=RelatorioFuncionarios.pdf");
                    Response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Response.Write("<script>alert('Erro ao gerar relatório.');</script>");
                }
            }
        }
        public void btnVoltar_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "Controles/ListaFuncionarios.ascx";
            Response.Redirect("Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
