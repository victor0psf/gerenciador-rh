using System;
using System.Web.UI;

namespace webforms_client
{
    public partial class Default : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserControlPath"] != null)
            {
                string path = Session["UserControlPath"].ToString();
                Control ctrl = LoadControl(path);
                ctrl.ID = "DynamicControl";
                phConteudo.Controls.Clear();
                phConteudo.Controls.Add(ctrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["UserControlPath"] == null)
            {
                Session["UserControlPath"] = "Controles/ListaFuncionarios.ascx";
                Response.Redirect("Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "Controles/NovoFuncionario.ascx";
            Response.Redirect("Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "Controles/EditarFuncionario.ascx";
            Response.Redirect("Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnCadastrarFerias_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "Controles/CadastrarFerias.ascx";
            Response.Redirect("Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnEditarFerias_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "Controles/EditarFerias.ascx";
            Response.Redirect("Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "Controles/Relatorio.ascx";
            Response.Redirect("Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        protected void btnVerFerias_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "~/Controles/VerFerias.ascx";
            Response.Redirect("Default.aspx");
        }

        protected void btnVerDetalhes_Click(object sender, EventArgs e)
        {
            Session["UserControlPath"] = "~/Controles/DetalhesFuncionario.ascx";
            Response.Redirect("Default.aspx");
        }

    }
}
