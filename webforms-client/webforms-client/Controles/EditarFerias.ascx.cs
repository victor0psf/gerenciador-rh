using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Web.UI;

namespace webforms_client.Controles
{
    public partial class EditarFerias : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["FeriasId"] != null)
            {
                int id = Convert.ToInt32(Session["FeriasId"]);
                CarregaFerias(id);
            }
        }

        private void CarregaFerias(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/");
                var response = client.GetAsync($"api/ferias/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    dynamic ferias = JsonConvert.DeserializeObject(json);

                    txtDataInicio.Text = Convert.ToDateTime(ferias.dataInicio.ToString()).ToString("yyyy-MM-dd");
                    txtDataTermino.Text = Convert.ToDateTime(ferias.dataTermino.ToString()).ToString("yyyy-MM-dd");
                    txtFuncionarioId.Text = ferias.funcionarioId.ToString();
                    hdnFuncionarioId.Value = ferias.funcionarioId.ToString();
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Session["FeriasId"] == null) return;

            int id = Convert.ToInt32(Session["FeriasId"]);
            int funcionarioId = Convert.ToInt32(
                string.IsNullOrWhiteSpace(hdnFuncionarioId.Value)
                    ? txtFuncionarioId.Text
                    : hdnFuncionarioId.Value
            );

            var ferias = new
            {
                dataInicio = txtDataInicio.Text,
                dataTermino = txtDataTermino.Text,
                funcionarioId = funcionarioId
            };

            string json = JsonConvert.SerializeObject(ferias);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PutAsync($"api/ferias/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Session["UserControlPath"] = "Controles/VerFerias.ascx";
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Write("<script>alert('Erro ao atualizar as férias.');</script>");
                }
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Session["FeriasId"] == null) return;

            int id = Convert.ToInt32(Session["FeriasId"]);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/");
                var response = client.DeleteAsync($"api/ferias/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    Session["UserControlPath"] = "Controles/VerFerias.ascx";
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Write("<script>alert('Erro ao excluir as férias.');</script>");
                }
            }
        }
    }
}
