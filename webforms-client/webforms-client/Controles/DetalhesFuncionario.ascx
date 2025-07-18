<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetalhesFuncionario.ascx.cs" Inherits="webforms_client.Controles.DetalhesFuncionario" %>

<div class="container">
    <div class="table-details">
    <h2>Funcionários Cadastrados</h2>
    <asp:GridView ID="gvDetalhes" runat="server" CssClass="tabela" AutoGenerateColumns="False">
        <Columns>
             <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="nome" HeaderText="Nome" />
            <asp:BoundField DataField="cargo" HeaderText="Cargo" />
            <asp:BoundField DataField="dataAdmissao" HeaderText="Admissão" DataFormatString="{0:dd/MM/yyyy}" />

            <asp:BoundField DataField="salarioAtual" HeaderText="Salário" DataFormatString="R$ {0:F2}" />

            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("status")) ? "Ativo" : "Inativo" %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <div class="media-salarial">
    <asp:Label ID="lblMediaSalarial" runat="server" CssClass="media-salarial" />
            </div>
        </div>
</div>
