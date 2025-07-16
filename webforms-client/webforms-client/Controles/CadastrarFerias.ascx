<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastrarFerias.ascx.cs" Inherits="webforms_client.Controles.CadastrarFerias" %>

<div class="form-container">
    <h2>Cadastrar Férias</h2>
    <div>
    <div class="form-group">
        <label for="txtDataInicio">Data Início:</label>
        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" TextMode="Date" />
    </div>

    <div class="form-group">
        <label for="txtDataFim">Data Término:</label>
        <asp:TextBox ID="txtDataFim" runat="server" CssClass="form-control" TextMode="Date" />
    </div>

    <div class="form-group">
        <label for="txtFuncionarioId">ID do Funcionário:</label>
        <asp:TextBox ID="txtFuncionarioId" runat="server" CssClass="form-control" Text="0" />
    </div>

    <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn-amarelo-full" OnClick="btnCadastrar_Click" />
  </div>
</div>
