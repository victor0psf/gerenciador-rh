<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListaFuncionarios.ascx.cs" Inherits="webforms_client.Controles.ListaFuncionarios" %>

<h2>Funcionários Cadastrados</h2>
<div class="table-details">
<asp:GridView ID="gvFuncionarios"
              runat="server"
              AutoGenerateColumns="false"
              CssClass="tabela-funcionarios"
              BorderStyle="None"
              EmptyDataText="Nenhum funcionário encontrado.">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Id" />
        <asp:BoundField DataField="Nome" HeaderText="Nome" />
        <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
        <asp:TemplateField HeaderText="Status">
    <ItemTemplate>
        <%# Convert.ToBoolean(Eval("Status")) ? "Ativo" : "Inativo" %>
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>