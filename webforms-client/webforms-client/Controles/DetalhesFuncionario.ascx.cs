using System;
using System.Net.Http;
using System.Web.UI;
using Newtonsoft.Json;

namespace webforms_client.Controles
{
    public partial class DetalhesFuncionario : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarFuncionarios();
                CarregarMediaSalarial();
            }
        }

        private void CarregarFuncionarios()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/api/");
                var response = client.GetAsync("funcionarios/webforms").Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    dynamic lista = JsonConvert.DeserializeObject<dynamic>(json);
                    gvDetalhes.DataSource = lista;
                    gvDetalhes.DataBind();
                }
                else
                {
                    gvDetalhes.EmptyDataText = "Erro ao carregar dados.";
                    gvDetalhes.DataBind();
                }
            }
        }

        private void CarregarMediaSalarial()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/api/");
                var response = client.GetAsync("funcionarios/media-salarial").Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    decimal media = JsonConvert.DeserializeObject<decimal>(json);
                    lblMediaSalarial.Text = $"Média Salarial Geral: R$ {media:F2}";
                }
            }
        }
    }
}
