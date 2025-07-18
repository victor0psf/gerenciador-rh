<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarFerias.ascx.cs" Inherits="webforms_client.Controles.EditarFerias" %>

<div class="form-container">
    <h2 class="form-title">Editar Férias</h2>

    <div class="form-group">
        <label for="txtDataInicio">Data Início:</label>
        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" TextMode="Date" />
    </div>

    <div class="form-group">
        <label for="txtDataTermino">Data Término:</label>
        <asp:TextBox ID="txtDataTermino" runat="server" CssClass="form-control" TextMode="Date" />
    </div>

    <div class="form-group">
        <label for="txtFuncionarioId">ID do Funcionário:</label>
        <asp:TextBox ID="txtFuncionarioId" runat="server" CssClass="form-control" ReadOnly="true" />
        <asp:HiddenField ID="hdnFuncionarioId" runat="server" />
    </div>

    <asp:Button ID="btnSalvar" runat="server" CssClass="btn-amarelo-full" Text="Salvar" OnClick="btnSalvar_Click" />
    <asp:Button ID="btnExcluir" runat="server" CssClass="btn-amarelo-full" Text="Excluir" OnClick="btnExcluir_Click" />
    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn-amarelo-full" OnClick="btnVoltar_Click" />
</div>

