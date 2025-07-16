using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace webforms_client.Controles
{
    public partial class NovoFuncionario : System.Web.UI.UserControl
    {
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            decimal salario = decimal.Parse(txtSalario.Text);

            var funcionario = new
            {
                nome = txtNome.Text,
                cargo = txtCargo.Text,
                dataAdmissao = txtAdmissao.Text,
                status = ddlStatus.SelectedValue == "true",
                salarios = new[] {
                    new {
                        salario = salario,
                        salarioMedio = salario
                    }
                }
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(funcionario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/funcionarios", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Session["UserControlPath"] = "Controles/ListaFuncionarios.ascx";
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Write("<script>alert('Erro ao cadastrar funcionário.');</script>");
                }
            }
        }
    }
}
