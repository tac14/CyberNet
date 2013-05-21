<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="CyberNet.Game.Stock" MasterPageFile="~/Site.Master"%>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True">
        <emptydatarowstyle forecolor="Red"/>
        <emptydatatemplate>
            У вас еще ничего нет  
        </emptydatatemplate>
    </asp:GridView>
</asp:Content>

