using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace webforms_client.Controles
{
    public partial class CadastrarFerias : System.Web.UI.UserControl
    {
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            var ferias = new
            {
                dataInicio = txtDataInicio.Text,
                dataTermino = txtDataFim.Text,
                funcionarioId = int.Parse(txtFuncionarioId.Text)
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(ferias);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/ferias", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Session["UserControlPath"] = "Controles/ListaFuncionarios.ascx";
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Write("<script>alert('Erro ao cadastrar férias.');</script>");
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
