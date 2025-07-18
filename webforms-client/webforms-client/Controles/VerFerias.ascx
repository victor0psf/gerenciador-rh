<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VerFerias.ascx.cs" Inherits="webforms_client.Controles.VerFerias" %>

<div class="container">
    <div class="table-details">
    <h2>Férias dos Funcionários</h2>
    <asp:GridView ID="gridFerias" runat="server" AutoGenerateColumns="false" CssClass="tabela-ferias">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="FuncionarioNome" HeaderText="Funcionário" />
            <asp:BoundField DataField="DataInicio" HeaderText="Início" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="DataTermino" HeaderText="Término" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="StatusFerias" HeaderText="Status" />
        </Columns>
    </asp:GridView>

        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn-amarelo-full" OnClick="btnVoltar_Click" />
        </div>
</div>

