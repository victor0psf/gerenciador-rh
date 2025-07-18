<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VerFerias.ascx.cs" Inherits="webforms_client.Controles.VerFerias" %>

<div class="container">
    <div class="table-details">
    <h2>Férias dos Funcionários</h2>
    <asp:GridView ID="gvFerias" runat="server" AutoGenerateColumns="false" CssClass="tabela">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID Férias" />
            <asp:BoundField DataField="funcionario.id" HeaderText="ID Funcionário" />
            <asp:BoundField DataField="funcionario.nome" HeaderText="Nome" />
            <asp:BoundField DataField="dataInicio" HeaderText="Início" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="dataTermino" HeaderText="Término" DataFormatString="{0:dd/MM/yyyy}" />
        </Columns>
    </asp:GridView>
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn-amarelo-full" OnClick="btnVoltar_Click" />
        </div>
</div>

