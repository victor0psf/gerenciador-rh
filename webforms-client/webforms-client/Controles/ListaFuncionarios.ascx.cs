using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI;

namespace webforms_client.Controles
{
    public partial class ListaFuncionarios : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaFuncionarios();
            }
        }

        private void CarregaFuncionarios()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5099/");
                    var response = client.GetAsync("api/funcionarios").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        var lista = JsonConvert.DeserializeObject<List<dynamic>>(json); // usando dynamic

                        gvFuncionarios.DataSource = lista;
                        gvFuncionarios.DataBind();
                    }
                    else
                    {
                        gvFuncionarios.EmptyDataText = $"Erro HTTP: {response.StatusCode}";
                        gvFuncionarios.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                gvFuncionarios.EmptyDataText = "Erro: " + ex.Message;
                gvFuncionarios.DataBind();
            }
        }
    }
}
