<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webforms_client.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Funcionários</title>
    <link href="Estilos/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Menu lateral -->
            <div class="menu-lateral">
                <asp:Button ID="btnNovo" runat="server" Text="+ Novo Funcionário" CssClass="btn-amarelo" OnClick="btnNovo_Click" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar Funcionários" CssClass="btn-amarelo" OnClick="btnEditar_Click" />
                <asp:Button ID="btnCadastrarFerias" runat="server" Text="Cadastrar Férias" CssClass="btn-amarelo" OnClick="btnCadastrarFerias_Click" />
                <asp:Button ID="btnEditarFerias" runat="server" Text="Editar Férias" CssClass="btn-amarelo" OnClick="btnEditarFerias_Click" />
                <asp:Button ID="btnRelatorio" runat="server" Text="Baixar Relatório" CssClass="btn-amarelo" OnClick="btnRelatorio_Click" />
            </div>

            <!-- Conteúdo principal -->
            <div class="conteudo">
                
                <!-- Botões centrais -->
                <div class="botoes-centro">
                    <asp:Button ID="btnVerFerias" runat="server" Text="Férias" CssClass="btn-amarelo" OnClick="btnVerFerias_Click" />
                    <asp:Button ID="btnVerDetalhes" runat="server" Text="Detalhes" CssClass="btn-amarelo" OnClick="btnVerDetalhes_Click" />
                </div>

                <!-- Área onde os controles são carregados dinamicamente -->
                <asp:PlaceHolder ID="phConteudo" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
