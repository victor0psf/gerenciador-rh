<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NovoFuncionario.ascx.cs" Inherits="webforms_client.Controles.NovoFuncionario" %>
<header>
    <link href="Estilos/estilo.css" rel="stylesheet" type="text/css" />
</header>
<div class="form-container">
    <h2> Novo Funcionário </h2>

    <div class="form-group">
        <label for="txtNome">Nome:</label>
        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label for="txtCargo">Cargo:</label>
        <asp:TextBox ID="txtCargo" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label for="txtAdmissao">Data de Admissão:</label>
        <asp:TextBox ID="txtAdmissao" runat="server" CssClass="form-control" TextMode="Date" />
    </div>

    <div class="form-group">
        <label for="ddlStatus">Status:</label>
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
            <asp:ListItem Text="Ativo" Value="true" />
            <asp:ListItem Text="Inativo" Value="false" />
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <label for="txtSalario">Salário:</label>
        <asp:TextBox ID="txtSalario" runat="server" CssClass="form-control" Text="0" />
    </div>

    <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar Funcionário" CssClass="btn-amarelo-full" OnClick="btnCadastrar_Click" />
      <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn-amarelo-full" OnClick="btnVoltar_Click" />
</div>
