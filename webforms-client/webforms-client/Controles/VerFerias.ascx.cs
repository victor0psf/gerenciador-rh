using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.UI;

namespace webforms_client.Controles
{
    public partial class VerFerias : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregarFerias();
        }

        private void CarregarFerias()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5099/");
                var response = client.GetAsync("api/ferias").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var lista = JsonConvert.DeserializeObject<List<dynamic>>(json);

                    gvFerias.DataSource = lista;
                    gvFerias.DataBind();
                }
            }
        }
    }
}
