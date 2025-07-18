﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Relatorio.ascx.cs" Inherits="webforms_client.Controles.Relatorio" %>

<div class="relatorio-container">
    <h2>Relatório de Funcionários</h2>

    <asp:Button ID="btnDownload" runat="server" Text="Baixar Relatório" CssClass="btn-amarelo" OnClick="btnDownload_Click" />
      <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn-amarelo-full" OnClick="btnVoltar_Click" />
</div>
