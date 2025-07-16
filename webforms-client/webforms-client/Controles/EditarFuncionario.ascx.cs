using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.UI;

namespace webforms_client
{
    public partial class EditarFuncionario : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Se você quiser preencher automaticamente usando sessão, mantenha esta parte
                if (Session["FuncionarioId"] != null)
                {
                    txtId.Text = Session["FuncionarioId"].ToString();
                    int id = Convert.ToInt32(Session["FuncionarioId"]);
                    CarregarFuncionario(id);
                }
            }
        }

        private void CarregarFuncionario(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/api/");
                var response = client.GetAsync($"funcionarios/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    dynamic funcionario = JsonConvert.DeserializeObject<dynamic>(json);

                    // Agora o ID não será preenchido automaticamente
                    txtNome.Text = funcionario.nome;
                    txtCargo.Text = funcionario.cargo;
                    txtDataAdmissao.Text = Convert.ToDateTime(funcionario.dataAdmissao).ToString("yyyy-MM-dd");
                    chkStatus.Checked = Convert.ToBoolean(funcionario.status);

                    if (funcionario.salarios != null && funcionario.salarios.HasValues)
                    {
                        txtSalario.Text = funcionario.salarios[0].salario.ToString();
                    }
                    else
                    {
                        txtSalario.Text = "0";
                    }
                }
                else
                {
                    // Tratar erro (opcional)
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                CarregarFuncionario(id);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            var updatePayload = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;

            if (!string.IsNullOrWhiteSpace(txtNome.Text))
                updatePayload["nome"] = txtNome.Text;

            if (!string.IsNullOrWhiteSpace(txtCargo.Text))
                updatePayload["cargo"] = txtCargo.Text;

            if (!string.IsNullOrWhiteSpace(txtDataAdmissao.Text))
            {
                if (DateTime.TryParse(txtDataAdmissao.Text, out DateTime dataAdmissao))
                    updatePayload["dataAdmissao"] = dataAdmissao.ToString("yyyy-MM-dd");
            }

            // Sempre enviar o status (checkbox)
            updatePayload["status"] = chkStatus.Checked;

            // Se o salário estiver preenchido e for válido
            if (decimal.TryParse(txtSalario.Text, out decimal salario) && salario > 0)
            {
                updatePayload["salarios"] = new[]
                {
            new { salario = salario, salarioMedio = salario }
        };
            }

            string json = JsonConvert.SerializeObject(updatePayload);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/api/");
                var response = client.PutAsync($"funcionarios/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Session["UserControlPath"] = "Controles/ListaFuncionarios.ascx";
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Write("<script>alert('Erro ao editar funcionário.');</script>");
                }
            }
        }

    }
}
