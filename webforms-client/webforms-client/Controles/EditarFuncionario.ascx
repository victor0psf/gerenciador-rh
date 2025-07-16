<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarFuncionario.ascx.cs" Inherits="webforms_client.EditarFuncionario" %>

<div class="form-container">
    <h2 class="titulo">Editar Funcionário</h2>

    <div class="form-group">
        <asp:Label ID="lblId" runat="server" Text="Id:" AssociatedControlID="txtId"  />
        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <asp:Label ID="lblNome" runat="server" Text="Nome:" AssociatedControlID="txtNome" />
        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <asp:Label ID="lblCargo" runat="server" Text="Cargo:" AssociatedControlID="txtCargo"/>
        <asp:TextBox ID="txtCargo" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <asp:Label ID="lblDataAdmissao" runat="server" Text="Data de Admissão:" AssociatedControlID="txtDataAdmissao"  />
        <asp:TextBox ID="txtDataAdmissao" runat="server" CssClass="form-control" TextMode="Date" />
    </div>

    <div class="form-group">
        <asp:Label ID="lblStatus" runat="server" Text="Status:" AssociatedControlID="chkStatus" />
        <asp:CheckBox ID="chkStatus" runat="server"/>
        <span style="color:white; font-size: 12px;">(Ativo = marcado)</span>
    </div>

    <div class="form-group">
        <asp:Label ID="lblSalario" runat="server" Text="Salário:" AssociatedControlID="txtSalario"  />
        <asp:TextBox ID="txtSalario" runat="server" CssClass="form-control" />
    </div>

    <asp:Button ID="btnSalvar" runat="server" Text="Salvar Alterações" CssClass="btn-amarelo" OnClick="btnSalvar_Click" />
</div>